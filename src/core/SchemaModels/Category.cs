using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.SchemaModels
{
    public class Category : ICategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("cateName")]
        public string CateName { get; set; }
        [BsonElement("desc")]
        public string Description { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        public Category()
        {
            Created = DateTime.UtcNow;
        }
    }

    public interface ICategory
    {
        string Id { get; set; }
        string CateName { get; set; }
        string Description { get; set; }
        DateTime Created { get; set; }
        string CreatorId { get; set; }
    }
}
