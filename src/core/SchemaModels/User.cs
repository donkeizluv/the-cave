using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaveCore.SchemaModels
{
    public enum UserRoles
    {
        User,
        Admin
    }
    public class User : IUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("pwd")]
        public string Pwd { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("roles")]
        public IEnumerable<UserRoles> Roles { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }

        public User()
        {
            Created = DateTime.UtcNow;
        }     
    }

    public interface IUser
    {
        string Id { get; set; }
        string Username { get; set; }
        string Pwd { get; set; }
        string Email { get; set; }
        IEnumerable<UserRoles> Roles { get; set; }
        DateTime Created { get; set; }
    }
}
