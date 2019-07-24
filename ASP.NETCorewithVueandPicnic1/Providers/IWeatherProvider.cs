using System.Collections.Generic;
using ASP.NETCorewithVueandPicnic1.Models;

namespace ASP.NETCorewithVueandPicnic1.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
