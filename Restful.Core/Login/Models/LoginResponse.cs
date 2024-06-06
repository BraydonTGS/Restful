namespace Restful.Core.Login.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid UserGuid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
