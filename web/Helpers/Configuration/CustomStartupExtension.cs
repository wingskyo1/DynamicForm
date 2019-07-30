using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicForm.Helpers.Configuration
{
    public static class CustomStartupExtension
    {
        public static CustomConfiguration AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            LogHelper.SetupLogger(configuration);
            var config = BindConfig(configuration);
            services.AddSingleton(config);

            return config;
        }

        private static CustomConfiguration BindConfig(IConfiguration configuration)
        {
            var dataConfig = new DataConfiguration();
            configuration.Bind("Data", dataConfig);
            var securityConfig = new SecurityConfiguration();
            configuration.Bind("Security", securityConfig);

            return new CustomConfiguration {Data = dataConfig, Security = securityConfig};
        }
    }
}
