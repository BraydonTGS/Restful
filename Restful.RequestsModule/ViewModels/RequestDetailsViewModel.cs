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

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private Request _request;

        [ObservableProperty]
        private string _results;
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

            SubmitButtonClicked = new DelegateCommand(async () => await OnSubmitButtonClickedExecuted());
            SaveButtonClicked = new DelegateCommand(async () => await OnSaveButtonClickedExecuted());
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(typeof(Request).Name, out Request request))
            {
                Results = string.Empty;
                Request = request;
            }
            Request ??= new Request(true);
        }

        private async Task OnSubmitButtonClickedExecuted()
        {
            try
            {
                if (!string.IsNullOrEmpty(Results)) Results = string.Empty;
                Results = await _apiService.ProcessRequestAsync(Request);
            }
            catch (Exception ex)
            {
                Results = string.Empty;
                _errorHandler.DisplayExceptionMessage(ex);
            }
        }

        private async Task OnSaveButtonClickedExecuted()
        {
            try
            {
                // Simulate Saving the Result to the DB //
                await Task.Delay(1000);

                _eventAggregator
                    .GetEvent<RequestSavedEvent>()
                    .Publish(Request);

            }
            catch (Exception ex)
            {
                Results = string.Empty;
                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
    }
}
