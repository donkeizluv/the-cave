using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic; 

namespace CaveCore.SchemaModels
{
    public class Post : IPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("image")]
        public byte[] Image { get; set; }

        [BsonElement("cate_id")]
        public string CateId { get; set; }

        [BsonElement("cate_name")]
        public string CateName { get; set; }

        [BsonElement("votes")]
        public IEnumerable<Vote> Votes {get; set;}

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        [BsonElement("creator_name")]
        public string CreatorName { get; set; }

        [BsonElement("comments")]
        public IEnumerable<Comment> Comments { get; set; }
        public Post()
        {
            Created = DateTime.UtcNow;
        }
    }

    public interface IPost
    {
        string Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        byte[] Image { get; set; }
        string CateId { get; set; }
        string CateName { get; set; }
        IEnumerable<Vote> Votes {get; set; }
        IEnumerable<Comment> Comments { get; set; }
        DateTime Created { get; set; }
        string CreatorId { get; set; }
        string CreatorName { get; set; }


    }
}
