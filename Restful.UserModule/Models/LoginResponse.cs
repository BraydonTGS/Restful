using Restful.Core.Interfaces;
using System;

namespace Restful.UserModule.Models
{
    public class LoginResponse : IResponse
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
