using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.SchemaModels;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace CaveCore.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _collection;


        public CategoryService(IOptions<DbSettings> option, IMapper mapper, IMongoClient dbClient)
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
            _collection = _db.GetCollection<Category>(_settings.CategoryCollectionName);

        }

        public async Task<string> Create(CategoryDto cate)
        {
            var exist = await _collection.Find(c => c.CateName == cate.CateName).AnyAsync();
            if (exist)
            {
                throw new BussinessException("Category with same name already existed");
            }
            var newCate = _mapper.Map<Category>(cate);
            await _collection.InsertOneAsync(newCate);
            return newCate.Id;
        }

        public async Task<IEnumerable<ICategory>> GetAllCates()
        {
            return await _collection
                            .Find(u => true)
                            .ToListAsync();
        }
        public async Task<CategoryDto> GetCateById(string cateId)
        {
            var cate = _mapper.Map<CategoryDto>(await _collection
                            .Find(u => u.Id == cateId)
                            .FirstOrDefaultAsync());
            return cate;
        }

        public async Task<string> GetCateNameById(string cateId)
        {
            
            var projection = Builders<Category>
                    .Projection
                    .Include(x => x.Id).Include(x => x.CateName);    
            return (await _collection
                            .Find(u => u.Id == cateId)
                            .Project<Category>(projection)
                            .FirstAsync()
                    ).CateName;
        }
    }
}