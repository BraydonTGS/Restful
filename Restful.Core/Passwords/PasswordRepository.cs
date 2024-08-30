using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;
using Restful.Global.Exceptions;

namespace Restful.Core.Passwords
{
    /// <summary>
    /// Password Repository
    /// </summary>
    public class PasswordRepository : BaseRepository<PasswordEntity, Guid>, IPasswordRepository
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public PasswordRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        #region GetByUserIdAsync
        /// <summary>
        /// Get the Password Entity by the Specified UserId
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<PasswordEntity?> GetByUserIdAsync(Guid key)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Set<PasswordEntity>()
                .Where(x => x.UserId == key && !x.IsDeleted)
                .FirstOrDefaultAsync();

            return result;
        }
        #endregion

        #region CreateAsync - Override
        /// <summary>
        /// When Creating a new Password in the Database, ensure one does not already exist for the Given User.
        /// 
        /// If so, Throw a PasswordAlreadyExistsException
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="PasswordAlreadyExistsException"></exception>
        public override async Task<PasswordEntity?> CreateAsync(PasswordEntity password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Check if a password for the user already exists
            bool passwordExists = await context.Passwords.AnyAsync(p => p.UserId == password.UserId);

            if (passwordExists)
                throw new PasswordAlreadyExistsException("A password for this user already exists.");

            var newEntry = await context.Passwords.AddAsync(password);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion
    }
}
