using Prism.Ioc;
using Serilog;
using Serilog.Events;
using System.IO;

namespace Restful.Core.Logging
{
    /// <summary>
    /// Serilog Configuration
    /// </summary>
    public class LoggingConfig
    {
        #region ConfigureLogging
        public static void ConfigureLogging(IContainerRegistry containerRegistry)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("Log", "Restful_Log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

            containerRegistry.RegisterInstance<ILogger>(logger);
        }
        #endregion
    }
}