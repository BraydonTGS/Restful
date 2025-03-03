﻿using Microsoft.EntityFrameworkCore;
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
using System.Net.Http;

namespace Restful.Core.Config
{
    /// <summary>
    /// Register Dependencies for the Restful Core Project
    /// </summary>
    public static class RegisterCoreAppServices
    {
        public static void RegisterCoreServices(IContainerRegistry containerRegistry, string dbName)
        {
            // Cache Manager //
            containerRegistry.RegisterSingleton<ICacheManager, CacheManager>();

            // Http Client //
            containerRegistry.RegisterScoped<HttpClient>();

            // Register DbContext Factory //
            containerRegistry.Register(typeof(IDbContextFactory<RestfulDbContext>), _ => new RestfulDbContextFactory(dbName));

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

        }
    }
}
