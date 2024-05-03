using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Api;
using Restful.RequestsModule.Models;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private Request _request;
        public DelegateCommand SubmitButtonClicked { get; set; }
        public DelegateCommand SaveButtonClicked { get; set; }
        public RequestDetailsViewModel(
            IRegionManager regionManager,
            IApiService apiService,
            IEventAggregator eventAggregator,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _apiService = apiService;
            _eventAggregator = eventAggregator;
            _errorHandler = errorHandler;

            Request = new Request(true);

            ConfigureDelegateCommands();
        }

        #region
        /// <summary>
        /// Configure the Delegate Commands
        /// </summary>
        private void ConfigureDelegateCommands()
        {
            SubmitButtonClicked = new DelegateCommand(
                (async () => await OnSubmitButtonClickedExecuted()), CanSubmitButtonClickedExecuted)
                .ObservesProperty(() => Request.Url);

            SaveButtonClicked = new DelegateCommand(
                (async () => await OnSaveButtonClickedExecuted()), CanSaveButtonClickedExecuted)
                .ObservesProperty(() => Request.Name);
        }
        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(typeof(Request).Name, out Request request))
            {
                Request = request;
            }
            Request ??= new Request(true);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) { Request = null; }

        private async Task OnSubmitButtonClickedExecuted()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.TempResult)) Request.TempResult = string.Empty;
                Request.TempResult = await _apiService.ProcessRequestAsync(Request);
            }
            catch (Exception ex)
            {
                Request.TempResult = string.Empty;
                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
        private bool CanSubmitButtonClickedExecuted() =>  !string.IsNullOrEmpty(Request.Url);


        #region OnSaveButtonClickedExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks Save
        /// </summary>
        /// <returns></returns>
        private async Task OnSaveButtonClickedExecuted()
        {
            try
            {
                // Simulate Saving the Result to the DB //
                await Task.Delay(500);

                // Validate the Request Being Saved //
                if (!string.IsNullOrEmpty(Request?.Name))
                    _eventAggregator
                        .GetEvent<RequestSavedEvent>()
                        .Publish(Request);
            }
            catch (Exception ex)
            {
                Request.TempResult = string.Empty;
                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
        private bool CanSaveButtonClickedExecuted() => !string.IsNullOrEmpty(Request.Name);
        #endregion
    }
}
