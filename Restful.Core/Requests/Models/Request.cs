using CommunityToolkit.Mvvm.ComponentModel;
using ICSharpCode.AvalonEdit.Document;
using Restful.Core.Base;
using Restful.Entity.Entities;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using HttpMethod = Restful.Global.Enums.HttpMethod;

namespace Restful.Core.Requests.Models
{
    public partial class Request : ModelBase<Guid>
    {
        [ObservableProperty]
        private string? _url;

        [ObservableProperty]
        private TextDocument? _body;

        [ObservableProperty]
        private TextDocument? _tempResult;

        [ObservableProperty]
        private HttpMethod _httpMethod;

        [ObservableProperty]
        private ObservableCollection<Parameter>? _parameters;

        [ObservableProperty]
        private ObservableCollection<Header>? _headers;

        [ObservableProperty]
        private Response? _response;

        public Guid CollectionId { get; set; }

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
        private bool _firstParam;
        private readonly StringBuilder _urlBuilder = new StringBuilder();
        private readonly StringBuilder _paramBuilder = new StringBuilder();
        /// <summary>
        /// Parameters Collection is Subscribed to this Event
        /// 
        /// Any Time the Parameters Collection is Updated, This Event Will Trigger
        /// 
        /// Dynamically Update the Request URL Based on the Current Parameters
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parameters_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Url))
                return;

            _urlBuilder.Clear();
            _paramBuilder.Clear();

            _urlBuilder.Append(Url.Split('?')[0]);

            if (Parameters == null || Parameters.Count == 0)
            {
                Url = _urlBuilder.ToString();
                return;
            }

            _firstParam = true;
            foreach (var parameter in Parameters)
            {
                if (parameter.CanAddToUrl())
                {
                    if (!_firstParam)
                        _paramBuilder.Append('&');

                    _paramBuilder.Append(parameter.ToString());

                    _firstParam = false;
                }
            }

            if (_paramBuilder.Length > 0)
            {
                _urlBuilder
                    .Append('?')
                    .Append(_paramBuilder);
            }

            Url = _urlBuilder.ToString();
            _firstParam = false;
        }
        #endregion

        #region Request_PropertyChanged
        /// <summary>
        /// Event that the PropertyChanged is Subscribed to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Request_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Url))
            {
                //Fire that logic //
            }
        }
        #endregion
    }
}
