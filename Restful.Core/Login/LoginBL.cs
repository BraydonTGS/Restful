using Restful.Core.Base;
using Restful.Core.Login.Models;
using Restful.Core.Passwords;
using Restful.Core.Users;
using Restful.Core.Users.Models;
using Restful.Entity.Entities;
using Restful.Global.Enums;
using Restful.Global.Exceptions;
using Serilog;

namespace Restful.Core.Login
{
    public class LoginBL : ILoginBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;
        private readonly IMapper<User, UserEntity> _mapper;
        private readonly ILogger _log;

        public LoginBL(
            IUserBL userBL,
            IPasswordBL passwordBL,
            ILogger logger,
            IMapper<User, UserEntity> mapper)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region LoginUserAsync
        /// <summary>
        /// Attempt to Login the Specified User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<LoginResponse?> LoginUserAsync(LoginRequest request)
        {
            _log.Information($"Starting LoginUserAsync for the Specified User.");
            try
            {
                var user = await _userBL.GetUserByUsernameAsync(request.Username);

                if (user is null)
                {
                    _log.Warning($"No User Found for with the Specified Username");
                    return null;
                }

                var success = await _passwordBL.VerifyUserPasswordAsync(user.Id, request.Password);

                if (success == PasswordVerificationResults.Failed)
                {
                    _log.Warning($"Password Verification Failure for the Specified User with the Email: {user.Email}");
                    throw new InvalidPasswordException($"Password Verification Failure for the Specified User");
                }

                var response = new LoginResponse(user, true);
                if (response is not null)
                    _log.Information($"Completed LoginUserAsync. Successfully Verified and Mapped the Specified User with the Email: {user.Email}.");

                return response;
            }
            catch (Exception ex)
            {
                _log.Error($"Error in LoginUserAsync with Message {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
