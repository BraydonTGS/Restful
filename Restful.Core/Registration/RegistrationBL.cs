using Restful.Core.Passwords;
using Restful.Core.Registration.Models;
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
        public async Task<RegistrationResponse> RegisterNewUserAsync(RegistrationRequest registrationRequest)
        {
            _log.Information($"Starting RegisterNewUserAsync for the New User.");
            try
            {

                var model = new User(
                    registrationRequest.FirstName,
                    registrationRequest.LastName,
                    registrationRequest.Email,
                    registrationRequest.Username,
                    registrationRequest.Password);

                model = await _userBL.CreateAsync(model);

                if (model is null)
                {
                    _log.Warning($"Unable to Register the New User with the UserName: {registrationRequest.Username}");
                    return new RegistrationResponse(false, "Unable to Register the New User");
                }

                await _passwordBL.CreatePasswordForUserAsync(model.Id, registrationRequest.Password);

                _log.Information($"Successfully Registered the New User!");

                var response = new RegistrationResponse(model, true);

                return response;
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
