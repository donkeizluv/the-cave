using System;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class CategoryDto
    {
        public string Id { get; set; }

        [Required]
        public string CatName { get; set; }

        // [Required]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string CreatorId { get; set; }
    }
}