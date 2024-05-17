using CommunityToolkit.Mvvm.ComponentModel;
using ICSharpCode.AvalonEdit.Document;
using Restful.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HttpMethod = Restful.Core.Enums.HttpMethod;

namespace Restful.RequestsModule.Models
{
    public partial class Request : ModelBase<Guid>
    {
        [ObservableProperty]
        private string _url;

        [ObservableProperty]
        private TextDocument _body;

        [ObservableProperty]
        private TextDocument _tempResult;

        [ObservableProperty]
        private HttpMethod _httpMethod;

        [ObservableProperty]
        private ObservableCollection<Parameter> _parameters;

        [ObservableProperty]
        private ObservableCollection<Header> _headers;

        [ObservableProperty]
        private Response _response;

        #region Constructor
        /// <summary>
        /// If True - Initialize a Request Object with Default Values
        /// </summary>
        /// <param name="initDefault"></param>
        public Request(bool initDefault = false)
        {
            if (initDefault)
            {
                InitializeDefaultHeaders();
                InitializeDefaultParameters();
                InitializeTextDocuments();
                PropertyChanged += Request_PropertyChanged;
            }

        }
        #endregion
        #region Initialize Default Request Properties
        /// <summary>
        /// Initialize the Request's Default Headers
        /// </summary>
        private void InitializeDefaultHeaders()
        {
            Headers = new ObservableCollection<Header>
            {
                new Header("Content-Type", "application/json", true),
                new Header("Accept", "*/*", true),
                new Header("Connection", "keep-alive", true)
            };
        }
        #endregion

        #region InitializeDefaultParameters
        /// <summary>
        /// Initialize the Request's Default Parameters
        /// </summary>
        private void InitializeDefaultParameters()
        {
            Parameters = new ObservableCollection<Parameter>();
            Parameters.CollectionChanged += Parameters_CollectionChanged;
        }
        #endregion

        #region InitializeTextDocuments
        /// <summary>
        /// Initialize the Request's Default Text Documents
        /// </summary>
        private void InitializeTextDocuments()
        {
            Body = new TextDocument();
            TempResult = new TextDocument();
        }
        #endregion

        #region Parameters_CollectionChanged
        /// <summary>
        /// Parameters Collection is Subscribed to this Event
        /// 
        /// Any Time the Parameters Collection is Updated, This Event Will Trigger
        /// 
        /// Dynamically Update the Request URL Based on the Current Parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Url))
                return;

            var baseUrl = Url.Split('?')[0];
            if (Parameters == null || Parameters.Count == 0)
            {
                Url = baseUrl;
                return;
            }

            var queryParams = Parameters
                .Where(x => x.Enabled && !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value))
                .Select(x => $"{x.Key}={x.Value}");

            if (queryParams.Any())
                Url = $"{baseUrl}?{string.Join("&", queryParams)}";
        }
        #endregion

        #region Request_PropertyChanged
        /// <summary>
        /// Event that the PropertyChanged is Subscribed to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Request_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Url))
            {
            }
        }
        #endregion
    }
}
