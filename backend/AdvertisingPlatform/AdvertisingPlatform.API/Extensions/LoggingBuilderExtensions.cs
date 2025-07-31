using Serilog;
using Serilog.Events;
using Shared.Logging;

namespace AdvertisingPlatform.API.Extensions
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder ConfigureLogger(
            this ILoggingBuilder logging,
            LogEventLevel consoleMinimumLevel = LogEventLevel.Verbose,
            bool writeToFile = true,
            string logFilePath = "logs/log.txt",
            LogEventLevel fileMinimumLevel = LogEventLevel.Verbose)
        {
            var configuration = new LoggerConfiguration();

            // Console
            configuration.WriteTo.Console(theme: CustomConsoleThemes.SixteenEnhanced, restrictedToMinimumLevel: consoleMinimumLevel);

            // File
            if (writeToFile && !string.IsNullOrEmpty(logFilePath))
                configuration.WriteTo.File(
                    path: logFilePath,
                    restrictedToMinimumLevel: fileMinimumLevel,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7);

            Log.Logger = configuration.CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog();

            return logging;
        }
    }
}
