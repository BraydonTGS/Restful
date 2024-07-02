using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Restful.Core.Base;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Requests.Models;
using Restful.Core.ViewModels;
using Restful.Global.Constant;
using Restful.RequestsModule.Views;
using System;
using System.Collections.ObjectModel;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsTreeViewModel : RegionViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private ObservableCollection<Request> _requests;

        public DelegateCommand<Request> RequestItemClicked { get; set; }
        public DelegateCommand AddNewRequest { get; set; }

        #region Constructor
        public RequestsTreeViewModel(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;

            Requests = new ObservableCollection<Request>();
            RequestItemClicked = new DelegateCommand<Request>(OnRequestItemClicked);
            AddNewRequest = new DelegateCommand(OnAddNewRequestExecuted);

            _eventAggregator
                .GetEvent<RequestSavedEvent>()
                .Subscribe(OnRequestSavedEventPublished);
        }
        #endregion

        #region OnNavigatedTo
        /// <inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                if (navigationContext.Parameters.TryGetValue(NavigationKeys.RequestsKey, out ObservableCollection<Request> requests))
                {
                    Requests = requests;
                    if (requests is null) Requests = [];
                }
            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        #endregion

        #region OnRequestItemClicked
        /// <summary>
        /// Command that is Fired when the User Clicks on a Request in the TreeView
        /// </summary>
        /// <param name="collection"></param>
        private void OnRequestItemClicked(Request request)
        {
            if (request != null)
            {
                var parameters = new NavigationParameters { { nameof(Request), request } };
                RequestNavigate(Regions.RequestDetailsRegion, nameof(RequestDetailsView), parameters);
            }
        }
        #endregion

        #region OnAddNewRequestExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks the Add New Request Button
        /// </summary>
        private void OnAddNewRequestExecuted()
        {
            var parameters = new NavigationParameters { { nameof(Request), new Request(true) } };
            RequestNavigate(Regions.RequestDetailsRegion, nameof(RequestDetailsView), parameters);
        }
        #endregion

        #region OnRequestSavedEventPublished
        /// <summary>
        /// Event that is Published when the User Saves a Request
        /// Only add a new Request to the Collection
        /// </summary>
        /// <param name="model"></param>
        private void OnRequestSavedEventPublished(IModel<Guid> model)
        {
            try
            {
                if (!Requests.Contains((Request)model))
                    Requests.Add((Request)model);
            }
            catch (Exception ex)
            {
                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
        #endregion
    }
}
