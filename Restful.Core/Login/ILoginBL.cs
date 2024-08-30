using Restful.Core.Login.Models;

namespace Restful.Core.Login
{
    public interface ILoginBL
    {
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
    }
}