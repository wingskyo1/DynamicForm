using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicForm.Helpers.Configuration
{
    public static class CustomStartupExtension
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            LogHelper.SetupLogger(configuration);
            var dataConfig = new DataConfiguration();
            configuration.Bind("Data", dataConfig);
            var config = new CustomConfiguration
            {
                Data = dataConfig,
            };
            services.AddSingleton(config);
        }
    }
}