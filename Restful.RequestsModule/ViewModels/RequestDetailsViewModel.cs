using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Restful.Core.Errors;
using Restful.Core.Events;
using Restful.Core.Files;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Core.Users;
using Restful.Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestDetailsViewModel : RegionViewModelBase
    {
        private readonly IRequestBL _requestBL;
        private readonly IRequestApiService _apiService;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileExportService _fileExportService;
        private readonly IErrorHandler _errorHandler;

        [ObservableProperty]
        private Request _request;
        public DelegateCommand SubmitButtonClicked { get; set; }
        public DelegateCommand SaveButtonClicked { get; set; }
        public DelegateCommand ExportButtonClicked { get; set; }
        public DelegateCommand RefreshButtonClicked { get; set; }

        #region Constructor
        public RequestDetailsViewModel(
            IRegionManager regionManager,
            IRequestBL requestBL,
            IRequestApiService apiService,
            IApplicationUserService applicationUserService,
            IEventAggregator eventAggregator,
            IFileExportService fileExportService,
            IErrorHandler errorHandler) : base(regionManager)
        {
            _requestBL = requestBL;
            _apiService = apiService;
            _applicationUserService = applicationUserService;
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

            if (Request?.TempResult != null)
                RefreshButtonClicked = new DelegateCommand(OnRefreshButtonClickedExecuted, CanRefreshButtonClickedExecuted)
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
                Request.UserId = _applicationUserService.GetApplicationUserGuid();

                await _requestBL.CreateAsync(Request);

                _eventAggregator.GetEvent<RequestSavedEvent>().Publish(Request);
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

        #region OnRefreshButtonClickedExecuted
        /// <summary>
        /// Command that is Executed when the User Clicks Refresh
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnRefreshButtonClickedExecuted()
        {
            Request.TempResult.Text = string.Empty;
        }
        private bool CanRefreshButtonClickedExecuted() => !string.IsNullOrEmpty(Request?.TempResult?.Text);
        #endregion
    }
}
