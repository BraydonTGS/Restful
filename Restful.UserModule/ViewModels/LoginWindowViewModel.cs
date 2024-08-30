using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Login;
using Restful.Core.Login.Models;
using Restful.Core.Users;
using Restful.Core.ViewModels;
using Restful.Global.Exceptions;
using Restful.UserModule.Views;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace Restful.UserModule.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private readonly ILoginBL _loginBL;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private LoginRequest _loginRequest;

        public DelegateCommand LoginUserCommand { get; set; }
        public DelegateCommand CreateNewUserCommand { get; set; }
        public DelegateCommand ResetPasswordCommand { get; set; }

        #region Constructor
        public LoginWindowViewModel(
            ILoginBL loginBL,
            IApplicationUserService applicationUserService,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler)
        {

            _loginBL = loginBL;
            _applicationUserService = applicationUserService;
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;

            LoginRequest = new LoginRequest();

            ConfigureDelegateCommands();
        }
        #endregion

        #region ConfigureDelegateCommands
        /// <summary>
        /// Configure the Delegate Commands
        /// </summary>
        private void ConfigureDelegateCommands()
        {
            LoginUserCommand = new DelegateCommand(
                (async () => await
                 OnLoginUserCommandAsyncExecuted()), CanLoginUserCommandExecuted)
                .ObservesProperty(() => LoginRequest.Username)
                .ObservesProperty(() => LoginRequest.Password);

            CreateNewUserCommand = new DelegateCommand(OnCreateNewUserCommandExecuted);
            ResetPasswordCommand = new DelegateCommand(OnResetPasswordCommandExecuted, CanResetPasswordCommandExecuted);
        }
        #endregion

        #region OnLoginUserCommandAsyncExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks Login
        /// 
        /// Only Enabled if the Username and Password Requirements are met
        /// </summary>
        /// <returns></returns>
        private async Task OnLoginUserCommandAsyncExecuted()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                var loginResponse = await _loginBL.LoginUserAsync(LoginRequest);
                if (loginResponse.IsSuccessful)
                {
                    _applicationUserService.SetApplicationUser(
                        loginResponse.User.Id, loginResponse.User.Username, loginResponse.User.Email);

                    _eventAggregator
                     .GetEvent<LoginSuccessEvent>()
                     .Publish(true);
                }

                else
                    MessageBox.Show($"{loginResponse.ErrorMessage}", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidPasswordException ex)
            {
                MessageBox.Show($"Error: {ex.Message}.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayExceptionMessage(ex);
            }
            finally { IsBusy = false; }
        }
        private bool CanLoginUserCommandExecuted() => LoginRequest.IsValid();
        #endregion

        #region 
        /// <summary>
        /// Command that is Executed when the User Clicks Register
        /// </summary>
        private void OnCreateNewUserCommandExecuted()
        {
            try
            {
                if (IsBusy) return;

                IsBusy = true;

                RegistrationWindow registrationWindow = new RegistrationWindow();
                registrationWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayExceptionMessage(ex);
            }
            finally { IsBusy = false; }

        }
        #endregion

        #region OnResetPasswordCommandExecuted
        /// <summary>
        /// WIP
        /// </summary>
        private void OnResetPasswordCommandExecuted() { }

        private bool CanResetPasswordCommandExecuted() => false;
        #endregion 
    }
}
