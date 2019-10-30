namespace CaveCore.Models
{
    public class UserValidateResultDto
    {
        public bool Valid { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
