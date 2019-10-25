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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;

        public CategoryService(IOptions<DbSettings> option, IMapper mapper, IMongoClient dbClient)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
        }

        public async Task Create(CategoryDto cat)
        {
            var catCollection = _db.GetCollection<Category>(_settings.CategoryCollectionName);
            var exist = await catCollection.Find(c => c.CatName == cat.CatName).AnyAsync();
            if (exist)
            {
                throw new BussinessException("Category with same name already existed");
            }
            await catCollection.InsertOneAsync(_mapper.Map<Category>(cat));
        }

        public async Task<IEnumerable<ICategory>> GetAll()
        {
            return await _db.GetCollection<Category>(_settings.CategoryCollectionName)
                            .Find(u => true)
                            .ToListAsync();
        }
    }
}