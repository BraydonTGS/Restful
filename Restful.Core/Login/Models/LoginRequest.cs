using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restful.Core.Login.Models
{
    public partial class LoginRequest : ObservableValidator
    {
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

        public LoginRequest() { ValidateAllProperties(); }
        
        public bool IsValid() => !HasErrors;
    }
}
