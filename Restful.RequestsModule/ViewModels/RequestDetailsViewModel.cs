using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Regions;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private Request _request;

        [ObservableProperty]
        private string _results;

        private readonly HttpClient _httpClient;

        public DelegateCommand SubmitButtonClicked { get; set; }
        public RequestDetailsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            _httpClient = new HttpClient();
            Request = new Request("Default");
            SubmitButtonClicked = new DelegateCommand(async () => await OnSubmitButtonClickedExecuted());
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(typeof(Request).Name, out Request request))
            {

            }
            if (Request is null)
                Request = new Request();
        }

        private async Task OnSubmitButtonClickedExecuted()
        {
            try
            {
                var url = Request.Url;
                if (!string.IsNullOrEmpty(url))
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var formattedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);
                    Results = formattedJson;
                }
            }
            catch (HttpRequestException e)
            {
                Results = $"Error: {e.Message}";
            }
            catch (Exception e)
            {
                Results = $"Unexpected error: {e.Message}";
            }
        }
    }
}
