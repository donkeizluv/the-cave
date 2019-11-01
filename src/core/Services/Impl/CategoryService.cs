using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.SchemaModels;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CaveCore.Services.Impl
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

        public async Task<string> Create(CategoryDto cate)
        {
            var catCollection = _db.GetCollection<Category>(_settings.CategoryCollectionName);
            var exist = await catCollection.Find(c => c.CateName == cate.CateName).AnyAsync();
            if (exist)
            {
                throw new BussinessException("Category with same name already existed");
            }
            var newCate = _mapper.Map<Category>(cate);
            await catCollection.InsertOneAsync(newCate);
            return newCate.Id;
        }

        public async Task<IEnumerable<ICategory>> GetAllCates()
        {
            return await _db.GetCollection<Category>(_settings.CategoryCollectionName)
                            .Find(u => true)
                            .ToListAsync();
        }
    }
}