using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;

namespace DynamicForm.Helpers
{
    public static class LogHelper
    {
        public static void SetupLogger(IConfiguration configuration)
        {
            var logPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "logs"), "log.txt");
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //Cross platform path issue makes us config File sink inside code
                .WriteTo.File(
                    new RenderedCompactJsonFormatter(),
                    logPath,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
            Log.Information($"Logger ready. Write to: {logPath}");
        }
    }
}