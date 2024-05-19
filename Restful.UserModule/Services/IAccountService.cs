using Restful.UserModule.Models;
using System.Threading.Tasks;

namespace Restful.UserModule.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}