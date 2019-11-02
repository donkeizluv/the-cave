using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.SchemaModels;
using CaveCore.Service.Impl;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace CaveCore.Services.Impl
{
    public class PostService : ContextAwareService, IPostService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Post> _collection;
        private readonly ICategoryService _cateservice;

        public PostService(IOptions<DbSettings> option,
                            IMapper mapper,
                            IMongoClient dbClient,
                            ClaimsPrincipal claimsPrincipal,
                            ICategoryService service) : base(claimsPrincipal)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
            _collection = _db.GetCollection<Post>(_settings.PostCollectionName);
            _cateservice = service;
        }

        public async Task AddPoint(string postId, int point)
        {
            var update = Builders<Post>.Update.Inc(p => p.Point, point);
            await _collection.UpdateOneAsync(p => p.Id == postId, update);
            return;
        }
        public async Task<string> Create(PostDto post)
        {
            var newPost = _mapper.Map<Post>(post);
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var cate = await _cateservice.GetCateById(post.CateId);
            if (cate == null) throw new BussinessException("Invalid category");
            newPost.CreatorId = CurrentId;
            newPost.CreatorName = CurrentUsername;
            newPost.CateName = (await _cateservice.GetCateNameById(post.CateId));
            newPost.Votes = new List<Vote>();
            await _collection.InsertOneAsync(_mapper.Map<Post>(newPost));
            return newPost.Id;
        }

        public async Task<string> Update(PostDto post)
        {
            var update = Builders<Post>.Update.Set(p => p.Content, post.Content).Set(p => p.Title, post.Title);
            await _collection.UpdateOneAsync(p => p.Id == post.Id, update);
            return post.Id;
        }

        public async Task<IEnumerable<IPost>> GetPostsByCateId(string cateId, int? order)
        {
            return order switch
            {
                1 => await _collection.Find(p => p.CateId == cateId).SortBy(p => p.Created).ToListAsync(),
                2 => await _collection.Find(p => p.CateId == cateId).SortByDescending(p => p.Created).ToListAsync(),
                3 => await _collection.Find(p => p.CateId == cateId).SortBy(p => p.Title).ToListAsync(),
                _ => await _collection.Find(p => p.CateId == cateId).SortByDescending(p => p.Point).ThenByDescending(p => p.MaxPoint).ToListAsync()
            };
        }

        public async Task<IEnumerable<IPost>> GetAllPosts(int? order)
        {
            return order switch
            {
                1 => await _collection.Find(p => true).SortBy(p => p.Created).ToListAsync(),
                2 => await _collection.Find(p => true).SortByDescending(p => p.Created).ToListAsync(),
                3 => await _collection.Find(p => true).SortBy(p => p.Title).ToListAsync(),
                _ => await _collection.Find(p => true).SortByDescending(p => p.Point).ThenByDescending(p => p.MaxPoint).ToListAsync()
            };
        }

        public async Task<IPost> GetPostById(string postId)
        {
            await AddPoint(postId, (int)PointEnum.View);
            return await _collection
                .Find(p => true && p.Id == postId)
                .FirstOrDefaultAsync();
        }
        public async Task<string> DeletePost(string postId)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var post = await postCollection.Find(p => p.Id == postId).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new BussinessException("Post does not exist to delete");
            }
            if (string.Compare(post.CreatorId, CurrentId, StringComparison.InvariantCulture) != 0)
            {
                throw new BussinessException("Unable to delete post of other user");
            }
            await postCollection.DeleteOneAsync(p => p.Id == post.Id);
            return post.Id;
        }
        public async Task<IEnumerable<IPost>> SearchPostWithCateId(string cateId, string searchText)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Regex(p => p.Title, searchText);
            if (cateId != null) filter &= builder.Eq(p => p.CateId, cateId);
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<IPost>> SearchAllCate(string searchText)
        {
            return await _collection
                .Find(Builders<Post>.Filter.Regex(p => p.Title, searchText)).ToListAsync();
        }
        public async Task CreateIndex()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Post>(Builders<Post>.IndexKeys.Ascending(x => x.Title)));
        }
        public async Task<IPost> AddVote(VoteRequestDto voteRequest)
        {
            await AddPoint(voteRequest.PostId, (int)PointEnum.UpVote);

            var reqPosId = voteRequest.PostId;
            var reqVoteType = voteRequest.VoteType;

            var post = await _collection.Find(p => p.Id == reqPosId).FirstOrDefaultAsync();
            if (post != null)
            {
                var votes = post.Votes != null ? post.Votes.ToList() : new List<Vote>();
                var foundVote = votes.Where(v => v.CreatorId == CurrentId).FirstOrDefault();
                if (foundVote == null)
                {
                    //add new
                    var newVote = new Vote(){
                        PostId = reqPosId,
                        VoteType = reqVoteType,
                        CreatorId = CurrentId
                    };
                    var updateDef = Builders<Post>.Update.Push(p => p.Votes, newVote);
                    await _collection.UpdateOneAsync(p => p.Id == reqPosId, updateDef);
                }else if(voteRequest.VoteType == foundVote.VoteType){
                    //remove
                    await _collection.FindOneAndUpdateAsync(p => p.Id == reqPosId, Builders<Post>.Update.PullFilter(p => p.Votes,
                                                v => v.CreatorId == CurrentId));

                }else {
                    //update
                     var filter = Builders<Post>.Filter.
                    And(Builders<Post>.Filter.
                    Eq(p => p.Id, voteRequest.PostId), Builders<Post>.Filter.
                    ElemMatch(p => p.Votes, p => p.Id == foundVote.Id));
                    await _collection.UpdateOneAsync(filter, Builders<Post>.Update.Set(p => p.Votes.ElementAt(-1).VoteType, voteRequest.VoteType));
                }
            }
            return await _collection.Find(p => p.Id == reqPosId).FirstOrDefaultAsync();
        }

        public async Task<string> AddComment(CommentDto comment)
        {
            await AddPoint(comment.PostId, (int)PointEnum.Comment);

            var reqPosId = comment.PostId;

            var newComment = new Comment
            {
                PostId = reqPosId,
                ParentId = comment.ParentId,
                CreatorId = CurrentId,
                Username = CurrentUsername,
                Content = comment.Content
            };

            var update = Builders<Post>.Update.Push(p => p.Comments, newComment);
            await _collection.UpdateOneAsync(p => p.Id == reqPosId, update);
            return newComment.Id;
        }

        public async Task<string> UpdateComment(CommentDto comment)
        {
            var filter = Builders<Post>.Filter.
                And(Builders<Post>.Filter.
                Eq(p => p.Id, comment.PostId), Builders<Post>.Filter.
                ElemMatch(p => p.Comments, p => p.Id == comment.Id));
            var update = Builders<Post>.Update.Set(p => p.Comments.ElementAt(-1).Content, comment.Content);
            await _collection.UpdateOneAsync(filter, update);
            return comment.Id;
        }

        public async Task<int> CountPostByCate(string cateId)
        {
            var cate = (await _collection.Find(p => p.CateId == cateId).ToListAsync());
            return (cate != null ? cate.Count(): 0);
        }
    }
}