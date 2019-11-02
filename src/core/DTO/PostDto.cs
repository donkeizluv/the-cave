using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CaveCore.DTO
{
    public class PostDto
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public string CateId { get; set; }

        public string CateName { get; set; }
        public long UpVotes { get; set; }

        public long DownVotes { get; set; }

        public IEnumerable<CommentDto> Comments {get; set;}

        public DateTime Created { get; set; }
        
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }


    }
}