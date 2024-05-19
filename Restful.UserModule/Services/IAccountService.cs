using Restful.UserModule.Models;
using System.Threading.Tasks;

namespace Restful.UserModule.Services
{
    public interface IAccountService
    {
        Task<object> LoginAsync(LoginRequest request);
    }
}