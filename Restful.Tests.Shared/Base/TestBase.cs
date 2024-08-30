using Microsoft.EntityFrameworkCore;
using Prism.DryIoc;
using Prism.Ioc;
using Restful.Core.Base;
using Restful.Core.Context;
using Restful.Core.Database;
using Restful.Core.Errors;
using Restful.Core.Files;
using Restful.Core.Headers;
using Restful.Core.Headers.Models;
using Restful.Core.Logging;
using Restful.Core.Login;
using Restful.Core.Parameters;
using Restful.Core.Parameters.Models;
using Restful.Core.Passwords;
using Restful.Core.Passwords.Model;
using Restful.Core.Registration;
using Restful.Core.Requests;
using Restful.Core.Requests.Models;
using Restful.Core.Users;
using Restful.Core.Users.Models;
using Restful.Entity.Entities;
using Restful.Tests.Shared.Context;

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
            containerRegistry.RegisterSingleton<IMapper<Password, PasswordEntity>, PasswordMapper>();
            containerRegistry.RegisterScoped<IMapper<User, UserEntity>, UserMapper>();
            containerRegistry.RegisterScoped<IMapper<Header, HeaderEntity>, HeaderMapper>();
            containerRegistry.RegisterScoped<IMapper<Parameter, ParameterEntity>, ParameterMapper>();

            // Register Business Services //
            containerRegistry.RegisterScoped<IRequestApiService, RequestApiService>();
            containerRegistry.Register<IPasswordHasher<Password>, PasswordHasher>();
            containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
            containerRegistry.RegisterSingleton<IApplicationUserService, ApplicationUserService>();
            containerRegistry.RegisterSingleton<IFileExportService, FileExportService>();
            containerRegistry.RegisterSingleton<IDatabaseManager, DatabaseManager>();

            // Repository //
            containerRegistry.RegisterScoped<IRequestRepository, RequestRepository>();
            containerRegistry.RegisterScoped<IPasswordRepository, PasswordRepository>();
            containerRegistry.RegisterScoped<IUserRepository, UserRepository>();
            containerRegistry.RegisterScoped<IHeaderRepository, HeaderRepository>();
            containerRegistry.RegisterScoped<IParameterRepository, ParameterRepository>();

            // BL //
            containerRegistry.RegisterScoped<IRequestBL, RequestBL>();
            containerRegistry.RegisterScoped<IPasswordBL, PasswordBL>();
            containerRegistry.RegisterScoped<IUserBL, UserBL>();
            containerRegistry.RegisterScoped<ILoginBL, LoginBL>();
            containerRegistry.RegisterScoped<IRegistrationBL, RegistrationBL>();
            containerRegistry.RegisterScoped<IRequestUpsertBL, RequestUpsertBL>();
            containerRegistry.RegisterScoped<IHeaderBL, HeaderBL>();
            containerRegistry.RegisterScoped<IParameterBL, ParameterBL>();

            // Logging //
            LoggingConfig.ConfigureLogging(containerRegistry);

            return containerRegistry;

        }
    }
}
