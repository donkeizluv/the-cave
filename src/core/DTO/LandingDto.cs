using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CaveCore.DTO
{
    public class LandingDto
    {

        public IEnumerable<CategoryDto> Categories { get; set; }

        public IEnumerable<PostListDto> TrendingPosts { get; set; }

    }
}