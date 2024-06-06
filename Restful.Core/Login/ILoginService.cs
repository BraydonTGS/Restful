using Restful.Core.Login.Models;

namespace Restful.Core.Login
{
    public interface ILoginService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}