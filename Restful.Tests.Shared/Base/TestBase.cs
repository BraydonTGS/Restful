using Microsoft.EntityFrameworkCore;
using Prism.DryIoc;
using Prism.Ioc;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Core.Database;
using Restful.Core.Errors;
using Restful.Core.Files;
using Restful.Core.Logging;
using Restful.Core.Login;
using Restful.Core.Passwords.Model;
using Restful.Core.Passwords;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Core.Users;
using Restful.Entity.Entities;
using Restful.Tests.Shared.Context;
using Restful.Tests.Shared.Database;

namespace Restful.Tests.Shared.Base
{
    public class TestBase
    {
        protected readonly IContainerProvider _containerProvider;

        public TestBase()
        {
            var containerExtension = new DryIocContainerExtension();
            RegisterTestServices(containerExtension);
            _containerProvider = containerExtension;
        }
        public static IContainerRegistry RegisterTestServices(IContainerRegistry containerRegistry)
        {
            // Http Client //
            containerRegistry.RegisterScoped<HttpClient>();

            // Register DbContext Factory //
            containerRegistry.RegisterScoped<IDbContextFactory<RestfulDbContext>, InMemoryContextFactory>();

            // Mappers //
            containerRegistry.RegisterSingleton<IMapper<Request, RequestEntity>, RequestMapper>();

            // Register Business Services //
            containerRegistry.RegisterScoped<ILoginBL, LoginBL>();
            containerRegistry.RegisterScoped<IRequestApiService, RequestApiService>();
            containerRegistry.Register<IPasswordHasher<Password>, PasswordHasher>();

            containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
            containerRegistry.RegisterSingleton<IApplicationUserService, ApplicationUserService>();
            containerRegistry.RegisterSingleton<IFileExportService, FileExportService>();
            containerRegistry.RegisterSingleton<IDatabaseManager, DatabaseManager>();

            // Repository //
            containerRegistry.RegisterScoped<IRequestRepository, RequestRepository>();
            containerRegistry.RegisterScoped<IRequestBL, RequestBL>();

            // Logging //
            LoggingConfig.ConfigureLogging(containerRegistry);

            return containerRegistry;

        }
    }
}
