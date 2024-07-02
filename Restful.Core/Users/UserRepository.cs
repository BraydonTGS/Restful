using Microsoft.EntityFrameworkCore;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Entity.Entities;
using Restful.Global.Exceptions;

namespace Restful.Core.Users
{
    public class UserRepository : BaseRepository<UserEntity, Guid>, IUserRepository
    {
        private readonly IDbContextFactory<RestfulDbContext> _contextFactory;
        public UserRepository(IDbContextFactory<RestfulDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region CreateAsync - Override
        /// <summary>
        /// When Creating a New User, Ensure that the Email Does not already Exist in the Database
        /// 
        /// If so, Throw an EmailAlreadyRegisteredException
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override async Task<UserEntity?> CreateAsync(UserEntity user)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Check if an email for the user already exists
            bool emailIsRegistered = await context
                .Set<UserEntity>()
                .AnyAsync(x => x.Email == user.Email);

            if (emailIsRegistered)
                throw new EmailAlreadyRegisteredException($"The Specified Email is already Registered");

            bool usernameIsRegistered = await context
                .Set<UserEntity>()
                .AnyAsync(x => x.UserName == user.UserName);

            if (usernameIsRegistered)
                throw new UsernameAlreadyRegisteredException($"The Specified Username is already Registered");

            var newEntry = await context
                .Set<UserEntity>()
                .AddAsync(user);

            await context.SaveChangesAsync();

            return newEntry.Entity;
        }
        #endregion

        #region GetUserByEmailAsync
        /// <summary>
        /// Get the User by Email - There can only be One Unique Email in the Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Set<UserEntity>()
                .FirstOrDefaultAsync(x => x.Email == email);

            return result;
        }
        #endregion

        #region GetUserByUsernameAsync
        /// <summary>
        /// Get the User by Username - There can only be One Unique Username in the Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserEntity?> GetUserByUsernameAsync(string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var result = await context.Set<UserEntity>()
                .FirstOrDefaultAsync(x => x.UserName == username && !x.IsDeleted);

            return result;
        }
        #endregion
    }
}
