using System.Collections.Generic;
using DynamicForm.Models;

namespace DynamicForm.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
