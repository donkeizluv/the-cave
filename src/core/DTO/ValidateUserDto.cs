using System.ComponentModel.DataAnnotations;

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