using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.SchemaModels
{
    public class Post : IPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("postTitle")]
        public string PostTitle { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("cate_id")]
        public string CateId { get; set; }

        [BsonElement("up_votes")]
        public long UpVotes { get; set; }        

        [BsonElement("down_votes")]
        public long DownVotes { get; set; }        

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        public Post()
        {
            Created = DateTime.UtcNow;
        }
    }

    public interface IPost
    {
        string Id { get; set; }
        string PostTitle { get; set; }
        string Content { get; set; }
        string CateId {get; set;}
        long UpVotes {get; set;}
        long DownVotes {get; set;}
        DateTime Created { get; set; }
        string CreatorId { get; set; }

 
    }
}
