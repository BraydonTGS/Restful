using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity, Guid>
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
    }
}
