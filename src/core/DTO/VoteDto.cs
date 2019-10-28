using System;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class VoteDto
    {
        public string Id { get; set; }
        [Required]
        public string PostId {get; set;}
        [Required]
        public string CreatorId {get; set;}
        [Required]
        public bool IsUpVote { get; set; }
    }
}