using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.Models
{
    public class Category : ICategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("catName")]
        public string CatName { get; set; }
        [BsonElement("desc")]
        public string Description { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }

        public Category()
        {
            Created = DateTime.UtcNow;
        }
    }

    public interface ICategory
    {
        string Id { get; set; }
        string CatName { get; set; }
        string Description { get; set; }
        DateTime Created { get; set; }
    }
}
