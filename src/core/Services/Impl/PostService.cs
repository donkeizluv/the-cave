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

        public async Task<string> Create(PostDto post)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var newPost = _mapper.Map<Post>(post);
            ICategory cate = await _cateservice.GetCateById(post.CateId);
            newPost.CreatorId = CurrentId;
            newPost.CreatorName = CurrentUsername;
            newPost.CateName = cate.CateName;
            await postCollection.InsertOneAsync(_mapper.Map<Post>(newPost));
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
                _ => await _collection.Find(p => p.CateId == cateId).ToListAsync()

            };
        }

        public async Task<IEnumerable<IPost>> GetAllPosts(int? order)
        {
            return order switch
            {
                1 => await _collection.Find(p => true).SortBy(p => p.Created).ToListAsync(),
                2 => await _collection.Find(p => true).SortByDescending(p => p.Created).ToListAsync(),
                3 => await _collection.Find(p => true).SortBy(p => p.Title).ToListAsync(),
                _ => await _collection.Find(p => true).ToListAsync()

            };
        }

        public async Task<IPost> GetPostById(string postId)
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
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

            var reqPosId = voteRequest.PostId;
            var reqVoteType = voteRequest.VoteType;

            var collection = _db.GetCollection<Post>(_settings.PostCollectionName);

            var post = await collection.Find(p => p.Id == reqPosId).FirstOrDefaultAsync();
            if (post != null)
            {
                var votes = post.Votes != null ? post.Votes.ToList() : new List<Vote>();
                var foundVote = votes.Where(v => v.CreatorId == CurrentId).FirstOrDefault();
                if (foundVote != null)
                {
                    votes.Remove(votes.Where(o => o.CreatorId == CurrentId).FirstOrDefault());
                }
                else
                {
                    var newVote = new Vote();
                    newVote.PostId = reqPosId;
                    newVote.VoteType = reqVoteType;
                    newVote.CreatorId = CurrentId;
                    votes.Add(newVote);
                }
                post.Votes = votes;
                var update = Builders<Post>.Update
                                            .Set(p => p.Votes, votes);
                await collection.UpdateOneAsync(p => p.Id == reqPosId, update);
            }
            return await collection.Find(p => p.Id == reqPosId).FirstOrDefaultAsync();
        }

        public async Task<string> AddComment(CommentDto comment)
        {
            var reqPosId = comment.PostId;
            var post = await _collection.Find(p => p.Id == reqPosId).FirstOrDefaultAsync();
            var comments = post.Comments != null ? post.Comments.ToList() : new List<Comment>();

            var newComment = new Comment
            {
                PostId = reqPosId,
                ParentId = comment.ParentId,
                CreatorId = CurrentId,
                Username = CurrentUsername,
                Content = comment.Content
            };
            comments.Add(newComment);
            var update = Builders<Post>.Update.Set(p => p.Comments, comments);
            await _collection.UpdateOneAsync(p => p.Id == reqPosId, update);

            return newComment.Id;
        }

        public async Task<string> UpdateComment(CommentDto comment)
        {
            var update = Builders<Post>.Update.Set(p => p.Comments.Where(c => c.Id == comment.Id).First().Content, comment.Content);
            await _collection.UpdateOneAsync(p => p.Id == comment.PostId, update);

            return comment.Id;
        }
    }
}