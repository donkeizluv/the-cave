using System;
using CaveCore.Models;

namespace CaveCore.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        public DateTime Created { get; set; }
    }
}