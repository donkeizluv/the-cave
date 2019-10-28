using System;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class PostDto
    {
        public string Id { get; set; }

        [Required]
        public string PostTitle { get; set; }

        [Required]
        public string Content { get; set; }
        
        [Required]
        public string CateId {get;set;}

        public long UpVotes {get;set;}

        public long DownVotes {get; set;}

        public DateTime Created { get; set; }

        public string CreatorId { get; set; }

    }
}