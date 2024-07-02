using Restful.Core.Base;
using Restful.Entity.Entities;

namespace Restful.Core.Passwords
{
    public interface IPasswordRepository : IBaseRepository<PasswordEntity, Guid>
    {
        Task<PasswordEntity?> GetByUserIdAsync(Guid key);
    }
}
