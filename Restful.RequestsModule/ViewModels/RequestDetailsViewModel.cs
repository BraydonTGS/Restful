using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Interfaces;
using Restful.Core.Services;
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
        private readonly IFileExportService _fileExportService;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private Request _request;
        public DelegateCommand SubmitButtonClicked { get; set; }
        public DelegateCommand SaveButtonClicked { get; set; }
        public DelegateCommand ExportButtonClicked { get; set; }

        #region Constructor
        public RequestDetailsViewModel(
            IRegionManager regionManager,
            IApiService apiService,
            IEventAggregator eventAggregator,
            IFileExportService fileExportService,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _apiService = apiService;
            _eventAggregator = eventAggregator;
            _fileExportService = fileExportService;
            _errorHandler = errorHandler;

            Request = new Request(true);

            ConfigureDelegateCommands();
        }
        #endregion

        #region ConfigureDelegateCommands
        /// <summary>
        /// Configure the Delegate Commands
        /// </summary>
        private void ConfigureDelegateCommands()
        {
            SubmitButtonClicked = new DelegateCommand(
                (async () => await OnSubmitButtonClickedAsyncExecuted()), CanSubmitButtonClickedExecuted)
                .ObservesProperty(() => Request.Url);

            SaveButtonClicked = new DelegateCommand(
                (async () => await OnSaveButtonClickedAsyncExecuted()), CanSaveButtonClickedExecuted)
                .ObservesProperty(() => Request.Name);

            if (Request?.TempResult != null)
                ExportButtonClicked = new DelegateCommand(OnExportButtonClickedExecuted, CanExportButtonClickedExecuted)
                    .ObservesProperty(() => Request.TempResult.Text);
        }
        #endregion

        #region OnNavigatedTo
        /// <inheritdoc/>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(typeof(Request).Name, out Request request))
            {
                Request = request;
            }
            Request ??= new Request(true);
        }
        #endregion

        #region OnNavigatedFrom
        /// <inheritdoc/>
        public override void OnNavigatedFrom(NavigationContext navigationContext) { Request = null; }
        #endregion

        #region OnSubmitButtonClickedExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks the Submit Button
        /// </summary>
        /// <returns></returns>
        private async Task OnSubmitButtonClickedAsyncExecuted()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request?.TempResult?.Text))
                    Request.TempResult.Text = string.Empty;

                Request.TempResult.Text = await _apiService.ProcessRequestAsync(Request);
            }
            catch (Exception ex)
            {
                if (Request?.TempResult != null)
                    Request.TempResult.Text = string.Empty;

                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
        private bool CanSubmitButtonClickedExecuted() => !string.IsNullOrEmpty(Request.Url);
        #endregion

        #region OnSaveButtonClickedExecuted
        /// <summary>
        /// Command that is Fired when the User Clicks Save
        /// </summary>
        /// <returns></returns>
        private async Task OnSaveButtonClickedAsyncExecuted()
        {
            try
            {
                // Simulate Saving the Result to the DB //
                await Task.Delay(200);

                // Validate the Request Being Saved //
                if (!string.IsNullOrEmpty(Request?.Name))
                    _eventAggregator
                        .GetEvent<RequestSavedEvent>()
                        .Publish(Request);
            }
            catch (Exception ex)
            {
                if (Request?.TempResult != null)
                    Request.TempResult.Text = string.Empty;

                _errorHandler.DisplayExceptionMessage(ex);
            }
        }
        private bool CanSaveButtonClickedExecuted() => !string.IsNullOrEmpty(Request.Name);
        #endregion

        #region OnExportButtonClickedExecuted
        /// <summary>
        /// Command that is Executed when the User Clicks Export
        /// </summary>
        private void OnExportButtonClickedExecuted()
        {
            try
            {
                var fullFilePath = _fileExportService.ChooseExportFilePath();

                if (!string.IsNullOrEmpty(fullFilePath))
                    _fileExportService.ExportJsonStringToFile(Request?.TempResult?.Text, fullFilePath);

            }
            catch (Exception ex) { _errorHandler.DisplayExceptionMessage(ex); }
        }
        private bool CanExportButtonClickedExecuted() => !string.IsNullOrEmpty(Request?.TempResult?.Text);
        #endregion
    }
}
