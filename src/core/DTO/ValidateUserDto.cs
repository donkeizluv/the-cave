using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class ValidateUserDto
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Pwd { get; set; }
    }
}