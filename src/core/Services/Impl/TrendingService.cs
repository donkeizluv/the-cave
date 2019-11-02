using System;
using System.Threading.Tasks;
using CaveCore.Exceptions;
using CaveCore.SchemaModels;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace CaveCore.Services.Impl
{
    public class TrendingService :  ITrendingService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<Post> _postCollection;
        private const double weigth = 0.12;
        private const double maxPointWeigth = 0.1;

        public TrendingService(IOptions<DbSettings> option,
                            IMongoClient dbClient) 
        {
            _settings = option.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_settings.DatabaseName);
            _postCollection = _db.GetCollection<Post>(_settings.PostCollectionName);        
        }
        public async Task<bool> ReCalculatePoint()
        {
            try{
                var post = await _postCollection.Find(p => true).ToListAsync();
                foreach(var p in post)
                {
                    var updateMaxPoint = Builders<Post>.Update.Set(o => o.MaxPoint, p.Point);
                    await _postCollection.UpdateOneAsync( i => i.Id == p.Id && i.Point > p.MaxPoint, updateMaxPoint);
                    var updateDef = Builders<Post>.Update.Set(o => o.Point, Math.Round(p.Point * (1 - weigth)));
                    await _postCollection.UpdateOneAsync(i => i.Id == p.Id && (p.Point * (1 - weigth) > p.MaxPoint * maxPointWeigth), updateDef);

                }
                return true;
            }catch (BussinessException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            } 
        }
    }
}
