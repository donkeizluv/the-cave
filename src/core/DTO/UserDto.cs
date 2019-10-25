using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CaveCore.SchemaModels;

namespace CaveCore.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Pwd { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; }

        public DateTime Created { get; set; }
    }
}