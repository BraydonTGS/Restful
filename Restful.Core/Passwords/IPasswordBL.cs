using Restful.Core.Passwords.Model;
using Restful.Global.Enums;

namespace Restful.Core.Passwords
{
    public interface IPasswordBL
    {
        Task<Password?> CreatePasswordForUserAsync(Guid userId, string password);
        Task<PasswordVerificationResults> VerifyUserPasswordAsync(Guid userId, string providedPassword);
    }
}
