using Microsoft.Extensions.DependencyInjection;

namespace DynamicForm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeather(this IServiceCollection services)
        {
            // All add weather related DI.
            services.AddSingleton<Providers.IWeatherProvider, Providers.WeatherProviderFake>();

            return services;
        }
    }
}
