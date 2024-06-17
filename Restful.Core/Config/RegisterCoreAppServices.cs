﻿using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Core.Database;
using Restful.Core.Errors;
using Restful.Core.Files;
using Restful.Core.Login;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Core.Users;
using Restful.Entity.Entities;
using System.Net.Http;

namespace Restful.Core.Config
{
    /// <summary>
    /// Register Dependencies for the Restful Core Project
    /// </summary>
    public static class RegisterCoreAppServices
    {
        public static void RegisterCoreServices(IContainerRegistry containerRegistry)
        {
            // Http Client //
            containerRegistry.RegisterScoped<HttpClient>();

            // Register DbContext Factory //
            containerRegistry.Register<IDbContextFactory<RestfulDbContext>, RestfulDbContextFactory>();

            // Mappers //
            containerRegistry.RegisterSingleton<IMapper<Request, RequestEntity>, RequestMapper>();

            // Register Business Services //
            containerRegistry.RegisterScoped<ILoginService, LoginService>();
            containerRegistry.RegisterScoped<IRequestApiService, RequestApiService>();

            containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
            containerRegistry.RegisterSingleton<IApplicationUserService, ApplicationUserService>();
            containerRegistry.RegisterSingleton<IFileExportService, FileExportService>();
            containerRegistry.RegisterSingleton<IDatabaseManager, DatabaseManager>();

            // Repository //
            containerRegistry.RegisterScoped<RequestRepository>();

        }
    }
}
