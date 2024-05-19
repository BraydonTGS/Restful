using CommunityToolkit.Mvvm.ComponentModel;

namespace Restful.UserModule.Models
{
    public partial class LoginRequest : ObservableObject
    {

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _email;
    }
}
