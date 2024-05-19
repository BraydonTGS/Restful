namespace Restful.UserModule.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string User { get; set; }
    }
}
