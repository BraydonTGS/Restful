﻿using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using Prism.Regions;
using Restful.Core.Constant;
using Restful.Core.ViewModels;
using Restful.RequestsModule.Models;
using Restful.RequestsModule.Views;
using System;
using System.Collections.ObjectModel;

namespace Restful.RequestsModule.ViewModels
{
    public partial class RequestsTreeViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Request> _requests;

        public DelegateCommand<Request> RequestItemClicked { get; set; }

        public RequestsTreeViewModel(
            IRegionManager regionManager) : base(regionManager)
        {
            RequestItemClicked = new DelegateCommand<Request>(OnRequestItemClicked);
        }

        #region OnNavigatedTo
        /// <summary>
        /// On Navigated To
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitTestData();
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
                var parameters = new NavigationParameters
                {
                    { nameof(Request), request }
                };

                RequestNavigate(Regions.RequestDetailsRegion, nameof(RequestDetailsView), parameters);
            }
        }
        #endregion

        #region InitTestData
        /// <summary>
        /// For Now, Init with Test Data when the Tree is Navigated to
        /// </summary>
        private void InitTestData()
        {
            Requests = new ObservableCollection<Request>
            {
                new Request { Id = Guid.NewGuid(), Name = "Get All" },
                new Request { Id = Guid.NewGuid(), Name = "GetById" },
                new Request { Id = Guid.NewGuid(), Name = "GetByIdIncludeCustomer" },
                new Request { Id = Guid.NewGuid(), Name = "CreateNew" },
                new Request { Id = Guid.NewGuid(), Name = "Delete" },
            };
        }
        #endregion
    }
}
