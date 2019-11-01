using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.SchemaModels
{
    public class Comment : IComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    
        [BsonElement("creator_id")]
        public string CreatorId {get; set;}

        [BsonElement("user_name")]
        public string Username { get; set; }

        [BsonElement("created")]
        public DateTime Created {get; set; }

        [BsonElement("post_id")]
        public string PostId { get; set; }

        [BsonElement("parent_id")]
        public string ParentId {get; set;}

        [BsonElement("content")]
        public string Content {get; set;}

        public Comment()
        {
            Created = DateTime.UtcNow;
            Id = ObjectId.GenerateNewId().ToString();
        }
    }

    public interface IComment
    {
        public string Id { get; set; }
    

        public string CreatorId {get; set;}

        public string Username { get; set; }

        public DateTime Created {get; set; }
        public string PostId { get; set; }

        public string ParentId {get; set;}

        public string Content {get; set;}
    }
}
