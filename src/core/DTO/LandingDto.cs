using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CaveCore.DTO
{
    public class LandingDto
    {

        public IEnumerable<CategoryDto> listCate { get; set; }

        public IEnumerable<PostDto> listPost { get; set; }

    }
}