using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.SchemaModels;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CaveCore.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;

        public PostService(IOptions<DbSettings> option, IMapper mapper, IMongoClient dbClient)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
        }

        public async Task<string> Create(PostDto post)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            // var exist = await catCollection.Find(p => p.PostTitle == post.PostTitle).AnyAsync();
            // if (exist)
            // {
            //     throw new BussinessException("Post with same name already existed");
            // }
            var newPost = _mapper.Map<Post>(post);
            await postCollection.InsertOneAsync(_mapper.Map<Post>(newPost));
            return newPost.Id;
        }

        public async Task<IEnumerable<IPost>> GetAllPostByCateId(string cateId)
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
                            .Find(p => true && p.CateId == cateId)
                            .ToListAsync();
        }

        public async Task<IEnumerable<IPost>> GetAllPost()
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IPost> GetPostById(string postId)
        {
            return await _db.GetCollection<Post>(_settings.PostCollectionName)
                .Find(p => true && p.Id == postId)
                .FirstAsync();
        }
        public async Task<string> DeletePost(string postId)
        {
            var postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);
            var exist = await postCollection.Find(p => p.Id == postId).AnyAsync();
            if (!exist)
            {
                throw new BussinessException("Post does not exist to delete.");
            }
            await postCollection.DeleteOneAsync(p => p.Id == postId);
            return "Post {0} is deleted successfully";
        }
    }
}