using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.SchemaModels
{
    public class Vote : IVote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("is_upvote")]
        public bool IsUpVote { get; set; }
        [BsonElement("post_id")]
        public string PostId { get; set; }

        [BsonElement("vote_type")]
        public int VoteType {get; set;}
        
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("creator_id")]
        public string CreatorId { get; set; }

        public Vote()
        {
            Created = DateTime.UtcNow;
            Id = ObjectId.GenerateNewId().ToString();
        }
    }

    public interface IVote
    {
        public string Id { get; set; }
    
        public string PostId {get; set;}

        public string CreatorId {get; set;}

        public bool IsUpVote { get; set; }

        public DateTime Created {get; set;}

        public int VoteType {get; set;}
    }
}
