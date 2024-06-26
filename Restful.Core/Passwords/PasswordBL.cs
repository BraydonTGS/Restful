using Restful.Core.Base;
using Restful.Core.Passwords.Model;
using Restful.Entity.Entities;
using Restful.Global.Enums;
using Serilog;

namespace Restful.Core.Passwords
{
    /// <summary>
    /// Password Business Logic
    /// </summary>
    public class PasswordBL : BaseBL<Password, PasswordEntity, Guid>, IPasswordBL
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IPasswordHasher<Password> _passwordHasher;
        public PasswordBL(
            IPasswordRepository passwordRepository,
            IPasswordHasher<Password> passwordHasher,
            IMapper<Password, PasswordEntity> mapper,
            ILogger log) : base(passwordRepository, mapper, log)
        {
            _passwordRepository = passwordRepository;
            _passwordHasher = passwordHasher;
        }

        #region CreatePasswordForUserAsync
        /// <summary>
        /// Create a Password Entity for the Specified User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Password?> CreatePasswordForUserAsync(Guid userId, string password)
        {
            _log.Information($"Starting CreatePasswordForUserAsync");
            try
            {
                var (hash, salt) = _passwordHasher.HashPassword(password);

                var dto = new Password(salt, hash);

                if (userId != Guid.Empty)
                    dto.UserId = userId;

                var entity = _mapper.Map(dto);

                entity = await _passwordRepository.CreateAsync(entity);

                if (entity is null)
                {
                    _log.Error("Failed to Create Password Entity with the Specified UserId");
                    return null;
                }

                dto = _mapper.Map(entity);

                _log.Information($"Completed CreatePasswordForUserAsync. Hashed, Created and Mapped the User's Password Successfully");
                return dto;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in CreatePasswordForUserAsync with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion

        #region VerifyUserPasswordAsync
        /// <summary>
        /// Verify the Specified User's Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public async Task<PasswordVerificationResults> VerifyUserPasswordAsync(Guid userId, string providedPassword)
        {
            _log.Information($"Starting VerifyUserPasswordAsync");
            try
            {
                var entity = await _passwordRepository.GetByUserIdAsync(userId);

                if (entity is null)
                {
                    _log.Warning($"No Password Exists for the Specified User.");
                    return PasswordVerificationResults.Failed;
                }

                var dto = _mapper.Map(entity);

                var results = _passwordHasher.VerifyHashedPassword(dto, providedPassword);

                _log.Information($"Completed VerifyUserPasswordAsync with Results: {results}");
                return results;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in VerifyUserPasswordAsync with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
