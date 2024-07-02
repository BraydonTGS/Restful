using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Registration;
using Restful.Core.Registration.Models;
using Restful.Core.Users;
using Restful.Core.ViewModels;
using Restful.Global.Exceptions;
using Restful.UserModule.Views;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Restful.UserModule.ViewModels
{
    public partial class RegistrationWindowViewModel : ViewModelBase
    {
        private readonly IRegistrationBL _registrationBL;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private RegistrationRequest _registrationRequest;

        public DelegateCommand RegisterNewUserCommand { get; set; }
        public DelegateCommand CancelRegistrationCommand { get; set; }
        public RegistrationWindowViewModel(
            IRegistrationBL registrationBL,
            IApplicationUserService applicationUserService,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler)
        {

            _registrationBL = registrationBL;
            _applicationUserService = applicationUserService;
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;

            RegistrationRequest = new RegistrationRequest();

            ConfigureDelegateCommands();
        }

        private void ConfigureDelegateCommands()
        {
            RegisterNewUserCommand = new DelegateCommand(
                (async () => await
                 OnRegisterNewUserCommandExecuted()), CanRegisterNewUserCommandExecuted)
                .ObservesProperty(() => RegistrationRequest.FirstName)
                .ObservesProperty(() => RegistrationRequest.LastName)
                .ObservesProperty(() => RegistrationRequest.Email)
                .ObservesProperty(() => RegistrationRequest.Username)
                .ObservesProperty(() => RegistrationRequest.Password)
                .ObservesProperty(() => RegistrationRequest.ConfirmPassword);

            CancelRegistrationCommand = new DelegateCommand(OnCancelRegistrationCommandExecuted);
        }

        private async Task OnRegisterNewUserCommandExecuted()
        {
            try
            {
                // Attempt to Register a New User //
                var registrationResponse = await _registrationBL.RegisterNewUserAsync(RegistrationRequest);

                if (registrationResponse.IsSuccessful)
                {
                    _applicationUserService.SetApplicationUser(
                     registrationResponse.User.Id, registrationResponse.User.Username, registrationResponse.User.Email);

                    // Close the registration window
                    CloseApplicationWindow(typeof(RegistrationWindow));

                    // User is Created so Publish a Successful Login Event //
                    _eventAggregator
                     .GetEvent<LoginSuccessEvent>()
                     .Publish(true);
                }

                else
                    MessageBox.Show("Could not Register New User", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidPasswordException)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        private bool CanRegisterNewUserCommandExecuted() => RegistrationRequest.IsValid();

        private void OnCancelRegistrationCommandExecuted() { CloseApplicationWindow(typeof(RegistrationWindow)); }
    }
}
