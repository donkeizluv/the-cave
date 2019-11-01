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

        public PostService(IOptions<DbSettings> option,
                            IMapper mapper,
                            IMongoClient dbClient,
                            ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
        }

        public async Task<string> Create(PostDto post)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var newPost = _mapper.Map<Post>(post);
            newPost.CreatorId = CurrentId;
            await postCollection.InsertOneAsync(_mapper.Map<Post>(newPost));
            return newPost.Id;
        }

        public async Task<IEnumerable<IPost>> GetPostsByCateId(string cateId)
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
                            .Find(p => true && p.CateId == cateId)
                            .ToListAsync();
        }

        public async Task<IEnumerable<IPost>> GetAllPosts()
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
                .Find(p => true)
                .ToListAsync();
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
    }
}