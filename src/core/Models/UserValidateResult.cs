namespace CaveCore.Models
{
    public class UserValidateResult : IUserValidateResult
    {
        public bool Valid { get; set; }
        public string Token { get; set; }
    }
    
    public interface IUserValidateResult
    {
        bool Valid { get; set; }
        string Token { get; set; }
    }

}
