using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Enums;
using Restful.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace Restful.RequestsModule.Models
{
    public partial class Request : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _url;

        [ObservableProperty]
        private HttpMethod _httpMethod;

        [ObservableProperty]
        private ObservableCollection<Parameters> _parameters;

        public Request() { }
        public Request(string requestName)
        { 
            Name = requestName;
            Parameters = new ObservableCollection<Parameters> 
            {
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty },
                new Parameters { Key = string.Empty, Value = string.Empty }
            };
        }
    }

    public partial class Parameters : ObservableObject
    {
        [ObservableProperty]
        private bool _enabled;

        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private string _description;
    }
}
