using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Regions;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Api;
using Restful.RequestsModule.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private Request _request;

        [ObservableProperty]
        private string _results;

        private readonly IApiService _apiService;

        public DelegateCommand SubmitButtonClicked { get; set; }
        public RequestDetailsViewModel(IRegionManager regionManager, IApiService apiService) : base(regionManager)
        {
            _apiService = apiService;
            Request = new Request("Default");
            SubmitButtonClicked = new DelegateCommand(async () => await OnSubmitButtonClickedExecuted());
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(typeof(Request).Name, out Request request))
            {
                Request = request;
            }
            Request ??= new Request();
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
                if (ex == null) return;
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
