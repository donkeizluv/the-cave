using System;
using System.ComponentModel.DataAnnotations;

namespace CaveCore.DTO
{
    public class CategoryDto
    {
        public string Id { get; set; }

        [Required]
        public string CateName { get; set; }

        // [Required]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string CreatorId { get; set; }

        public int PostCount { get;set; }
    }
}