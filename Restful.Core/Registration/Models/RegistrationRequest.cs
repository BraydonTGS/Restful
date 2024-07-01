using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restful.Core.Registration.Models
{
    public partial class RegistrationRequest : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(1, ErrorMessage = "First Name must be at least 1 character.")]
        [MaxLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        [Required(ErrorMessage = "First Name is required.")]
        private string _firstName = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(1, ErrorMessage = "Last Name must be at least 1 character.")]
        [MaxLength(150, ErrorMessage = "Last Name cannot exceed 150 characters.")]
        [Required(ErrorMessage = "Last Name is required.")]
        private string _lastName = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [EmailAddress]
        [MinLength(6, ErrorMessage = "Email must be at least 6 character.")]
        [MaxLength(250, ErrorMessage = "Email cannot exceed 250 characters.")]
        [Required(ErrorMessage = "Email is required.")]
        private string _email = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(8, ErrorMessage = "Username must be at least 8 character.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Username is required.")]
        private string _username = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Password is required.")]
        private string _password = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(8, ErrorMessage = "Confirm Password must be at least 8 characters.")]
        [MaxLength(50, ErrorMessage = "Confirm Password cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        private string _confirmPassword = string.Empty;

        public RegistrationRequest() { ValidateAllProperties(); }

        public bool IsValid() => !HasErrors && Password == ConfirmPassword;
    }
}
