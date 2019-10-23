using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.Settings;
using MongoDB.Driver;

namespace CaveCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;

        public CategoryService(IDbSettings settings, IMapper mapper)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
        }

        public async Task Create(CategoryDto cat)
        {
            await _db.GetCollection<Category>(_settings.CategoryCollectionName)
                    .InsertOneAsync(_mapper.Map<Category>(cat));
        }

        public async Task<IEnumerable<ICategory>> GetAll()
        {
            return await _db.GetCollection<Category>(_settings.CategoryCollectionName)
                            .Find(u => true)
                            .ToListAsync();
        }
    }
}