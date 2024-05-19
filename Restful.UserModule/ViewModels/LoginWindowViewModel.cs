using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.ViewModels;
using Restful.UserModule.Models;
using Restful.UserModule.Services;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace Restful.UserModule.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private readonly IAccountService _accountService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private LoginRequest _loginRequest;

        public DelegateCommand LoginUserCommand { get; set; }
        public DelegateCommand CreateNewUserCommand { get; set; }
        public DelegateCommand ResetPasswordCommand { get; set; }
        public LoginWindowViewModel(
            IAccountService accountService,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler)
        {

            _accountService = accountService;
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

            CreateNewUserCommand = new DelegateCommand(OnCreateNewUserCommandExecuted, CanExecute);
            ResetPasswordCommand = new DelegateCommand(OnResetPasswordCommandExecuted, CanExecute);
        }

        private async Task OnLoginUserCommandAsyncExecuted()
        {
            try
            {
                // Dummy check

                var currentUser = await _accountService.LoginAsync(LoginRequest);
                if (currentUser is not null)
                    _eventAggregator
                        .GetEvent<LoginSuccessEvent>()
                        .Publish(true);

                else
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }


        }

        private bool CanLoginUserCommandExecuted()
            => !string.IsNullOrEmpty(LoginRequest.Username)
            && !string.IsNullOrEmpty(LoginRequest.Password);

        private void OnCreateNewUserCommandExecuted() { }

        private void OnResetPasswordCommandExecuted() { }

        private bool CanExecute() => false;
    }
}
