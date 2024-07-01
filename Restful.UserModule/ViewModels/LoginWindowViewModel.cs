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

        private void ConfigureDelegateCommands()
        {
            LoginUserCommand = new DelegateCommand(
                (async () => await
                 OnLoginUserCommandAsyncExecuted()), CanLoginUserCommandExecuted)
                .ObservesProperty(() => LoginRequest.Username)
                .ObservesProperty(() => LoginRequest.Password);

            CreateNewUserCommand = new DelegateCommand(OnCreateNewUserCommandExecuted);
            ResetPasswordCommand = new DelegateCommand(OnResetPasswordCommandExecuted, CanExecute);
        }

        private async Task OnLoginUserCommandAsyncExecuted()
        {
            try
            {
                // Attempt to Login the User //
                // responses 
                var loginResponse = await _loginBL.LoginUserAsync(LoginRequest);
                if (loginResponse is not null && loginResponse.IsSuccessful)
                {
                    _applicationUserService.SetApplicationUser(
                        loginResponse.User.Id, loginResponse.User.Username, loginResponse.User.Email);

                    _eventAggregator
                     .GetEvent<LoginSuccessEvent>()
                     .Publish(true);
                }

                else
                    MessageBox.Show("Username Not Found", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidPasswordException)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }

        private bool CanLoginUserCommandExecuted()
            => !string.IsNullOrEmpty(LoginRequest.Username)
            && !string.IsNullOrEmpty(LoginRequest.Password);

        private void OnCreateNewUserCommandExecuted() 
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }

        private void OnResetPasswordCommandExecuted() { }

        private bool CanExecute() => false;
    }
}
