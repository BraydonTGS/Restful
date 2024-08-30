using Restful.Core.Base;
using Restful.Core.Users.Models;
using Restful.Entity.Entities;

namespace Restful.Core.Users
{
    public interface IUserBL : IBaseBL<User, UserEntity, Guid>
    {
        public Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
