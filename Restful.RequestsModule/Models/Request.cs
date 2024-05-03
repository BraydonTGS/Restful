using CommunityToolkit.Mvvm.ComponentModel;
using Restful.Core.Models;
using System;
using System.Collections.ObjectModel;
using HttpMethod = Restful.Core.Enums.HttpMethod;

namespace Restful.RequestsModule.Models
{
    public partial class Request : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _url;

        [ObservableProperty]
        private HttpMethod _httpMethod;

        [ObservableProperty]
        private ObservableCollection<Parameter> _parameters;

        [ObservableProperty]
        private ObservableCollection<Header> _headers;

        [ObservableProperty]
        private string _tempResult;

        [ObservableProperty]
        private Response _response;

        public Request() { }
        public Request(bool initDefault = false)
        {
            if (initDefault)
            {
                InitializeDefaultHeaders();
                InitializeDefaultParameters();
            }

        }

        private void InitializeDefaultHeaders()
        {
            Headers = new ObservableCollection<Header>
            {
                new Header("Content-Type", "application/json", true),
                new Header("Accept", "*/*", true),
                new Header("Connection", "keep-alive", true)
            };
        }

        private void InitializeDefaultParameters()
        {
            Parameters = new ObservableCollection<Parameter>();
        }
    }
}
