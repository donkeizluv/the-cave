using System.ComponentModel.DataAnnotations;

namespace CaveCore.DTO
{
    public class CommentDto
    {
        public string Id { get; set; }

        [Required]
        public string PostId { get; set; }
        [Required]
        public string Content { get; set; }
        public string ParentId { get; set; }
        public string Username { get; set; }
        public string Created { get; set; }
    }
}
