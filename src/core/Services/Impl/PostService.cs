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

namespace CaveCore.Services.Impl
{
    public class PostService : ContextAwareService, IPostService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Post> _collection;

        public PostService(IOptions<DbSettings> option,
                            IMapper mapper,
                            IMongoClient dbClient,
                            ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
            _collection = _db.GetCollection<Post>(_settings.PostCollectionName);
        }

        public async Task<string> Create(PostDto post)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var newPost = _mapper.Map<Post>(post);
            newPost.CreatorId = CurrentId;
            await postCollection.InsertOneAsync(_mapper.Map<Post>(newPost));
            return newPost.Id;
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
    }
}