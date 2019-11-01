using System.ComponentModel.DataAnnotations;


namespace CaveCore.DTO
{
    public class VoteRequestDto
    {
        [Required]
        public string PostId {get; set;}
        [Required]
        public int VoteType {get; set;}
    }
}