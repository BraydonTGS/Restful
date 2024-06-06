﻿using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Requests.Models;

namespace Restful.Core.Login.Models
{
    public partial class LoginRequest : ObservableObject
    {

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;
    }
}
