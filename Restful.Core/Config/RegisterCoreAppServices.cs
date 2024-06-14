using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Restful.Core.Context;
using Restful.Core.Database;
using Restful.Core.Errors;
using Restful.Core.Files;
using Restful.Core.Login;
using Restful.Core.Requests;
using Restful.Core.Users;
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
            // Register DbContext Factory //
            containerRegistry.Register<IDbContextFactory<RestfulDbContext>, RestfulDbContextFactory>();

            // Register Services //
            containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
            containerRegistry.RegisterSingleton<IApplicationUserService, ApplicationUserService>();
            containerRegistry.RegisterSingleton<IFileExportService, FileExportService>();
            containerRegistry.RegisterSingleton<IDatabaseManager, DatabaseManager>();


            containerRegistry.RegisterScoped<HttpClient>();
            containerRegistry.RegisterScoped<ILoginService, LoginService>();
            containerRegistry.RegisterScoped<IRequestApiService, RequestApiService>();
        }
    }
}
