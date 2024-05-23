using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Interfaces;

namespace Restful.UserModule.Models
{
    public partial class LoginRequest : ObservableObject, IRequest
    {

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _email;
    }
}
