using System;

namespace Restful.UserModule.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid UserGuid { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
