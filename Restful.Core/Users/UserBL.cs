using Restful.Core.Base;
using Restful.Core.Users.Models;
using Restful.Entity.Entities;
using Serilog;

namespace Restful.Core.Users
{
    public class UserBL : BaseBL<User, UserEntity, Guid>, IUserBL
    {
        private readonly IUserRepository _userRepository;
        public UserBL(
           IUserRepository userRepository,
           IMapper<User, UserEntity> mapper,
           ILogger log) : base(userRepository, mapper, log)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        #region CreateAsync - Override
        /// <summary>
        /// Creates a new  User  Model asynchronously.
        /// </summary>
        /// <param name="model">The Model to be created.</param>
        /// <returns>The created Model after it is added to the database.</returns>
        public override async Task<User?> CreateAsync(User model)
        {
            _log.Information($"Starting CreateAsync for the Specified User.");
            try
            {
                var entity = _mapper.Map(model);
                if (entity == null)
                {
                    _log.Warning($"Unable to Map Model of Type UserModel to User.");
                    return null;
                }

                var createdEntity = await _userRepository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create Entity of Type User in Database.");
                    return null;
                }

                model = _mapper.Map(createdEntity);

                _log.Information($"Completed CreateAsync for UserModel. Entity Creation and Mapping Successful.");
                return model;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreateAsync for UserModel with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region GetUserByEmailAsync
        /// <summary>
        /// Get the User by the Specified Email Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            _log.Information($"Starting GetUserByEmailAsync");
            try
            {
                var entity = await _userRepository.GetUserByEmailAsync(email);

                if (entity == null)
                {
                    _log.Warning($"No Email Exists for the Specified User.");
                    return null;
                }

                var model = _mapper.Map(entity);
                _log.Information($"Completed GetUserByEmailAsync. Successfully retrieved the Specified User with email: {email}");

                return model;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in GetUserByEmailAsync with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
