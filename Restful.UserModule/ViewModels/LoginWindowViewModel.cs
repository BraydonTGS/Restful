using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Restful.Core.Events;
using Restful.Core.ViewModels;
using System.Windows;


namespace Restful.UserModule.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public DelegateCommand LoginUserCommand { get; set; }
        public DelegateCommand CreateNewUserCommand { get; set; }
        public DelegateCommand ResetPasswordCommand { get; set; }
        public LoginWindowViewModel(
            IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            ConfigureDelegateCommands();
        }

        private void ConfigureDelegateCommands()
        {
            LoginUserCommand = new DelegateCommand(
                OnLoginUserCommandExecuted, CanLoginUserCommandExecuted)
                .ObservesProperty(() => Username)
                .ObservesProperty(() => Password);

            CreateNewUserCommand = new DelegateCommand(OnCreateNewUserCommandExecuted, CanExecute);
            ResetPasswordCommand = new DelegateCommand(OnResetPasswordCommandExecuted, CanExecute);
        }

        private void OnLoginUserCommandExecuted()
        {
            // Dummy check
            if (DummyLoginUser())
            {
                _eventAggregator.GetEvent<LoginSuccessEvent>().Publish();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanLoginUserCommandExecuted() => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        private void OnCreateNewUserCommandExecuted() { }

        private void OnResetPasswordCommandExecuted() { }

        private bool CanExecute() => false;

        private bool DummyLoginUser()
        {
            return Username?.ToLower() == "admin" && Password?.ToLower() == "password";
        }
    }
}
