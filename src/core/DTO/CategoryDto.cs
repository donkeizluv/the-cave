using System;
using CaveCore.Models;

namespace CaveCore.DTO
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}