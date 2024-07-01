using Restful.Core.Passwords;
using Restful.Core.Users;
using Restful.Core.Users.Models;
using Serilog;

namespace Restful.Core.Registration
{
    public class RegistrationBL : IRegistrationBL
    {
        private readonly IUserBL _userBL;
        private readonly IPasswordBL _passwordBL;
        private readonly ILogger _log;

        public RegistrationBL(
            IUserBL userBL,
            IPasswordBL passwordBL,
            ILogger logger)
        {
            _userBL = userBL ?? throw new ArgumentNullException(nameof(userBL));
            _passwordBL = passwordBL ?? throw new ArgumentNullException(nameof(passwordBL));
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region RegisterNewUserAsync
        /// <summary>
        /// Create a New User
        /// 
        /// Hash the User's Temp Password
        /// 
        /// Create the Password
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User?> RegisterNewUserAsync(User user)
        {
            _log.Information($"Starting RegisterNewUserAsync for the New User.");
            try
            {
                if (user is null) return null;

                var model = await _userBL.CreateAsync(user);

                if (model is null)
                {
                    _log.Warning($"Unable to Register the New User with the UserName: {user.Username}");
                    return null;
                }

                await _passwordBL.CreatePasswordForUserAsync(model.Id, user.TempPassword);

                _log.Information($"Successfully Registered the New User!");
                return model;
            }
            catch (Exception ex)
            {
                _log.Error($"Exception in RegisterNewUserAsync for with Message: {ex.Message}.");
                throw;
            }
        }
        #endregion
    }
}
