using Restful.Core.Base;
using Restful.Global.Enums;

namespace Restful.Core.Passwords
{
    /// <summary>
    /// Interface used to define methods needed for Hashing and Salting
    /// </summary>
    public interface IPasswordHasher<TPassword> where TPassword : ModelBase<Guid>
    {
        public (byte[] hash, byte[] salt) HashPassword(string password);

        public PasswordVerificationResults VerifyHashedPassword(TPassword password, string providedPassword);
    }
}
