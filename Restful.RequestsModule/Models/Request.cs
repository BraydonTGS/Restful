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
        private ObservableCollection<Parameter> _parameters;

        [ObservableProperty]
        private ObservableCollection<Header> _headers;

        public Request() { }
        public Request(string requestName)
        {
            Name = requestName;
            InitializeDefaultHeaders();
            InitializeDefaultParameters();
        }

        private void InitializeDefaultHeaders()
        {
            Headers = new ObservableCollection<Header>
            {
                new Header("Content-Type", "application/json", true),
                new Header("Content-Length", "<calculated when request is sent>", true),
                new Header("Host", "<calculated when request is sent>", true),
                new Header("Accept", "*/*", true),
                new Header("Accept-Encoding", "gzip, deflate, br", true),
                new Header("Connection", "keep-alive", true)
            };
        }

        private void InitializeDefaultParameters()
        {
            Parameters = new ObservableCollection<Parameter>();
        }
    }
}
