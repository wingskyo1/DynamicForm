using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DynamicForm.Providers;

namespace DynamicForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController2 : ControllerBase
    {
        private readonly IWeatherProvider weatherProvider;

        public WeatherController2(IWeatherProvider weatherProvider) { this.weatherProvider = weatherProvider; }

        [HttpGet("[action]")]
        public IActionResult Forecasts([FromQuery(Name = "from")] int from = 0, [FromQuery(Name = "to")] int to = 4)
        {
            var quantity = to - from;

            // We should also avoid going too far in the list.
            if (quantity <= 0)
            {
                return BadRequest("You cannot have the 'to' parameter higher than 'from' parameter.");
            }

            if (from < 0) { return BadRequest("You cannot go in the negative with the 'from' parameter"); }

            var allForecasts = weatherProvider.GetForecasts();
            var result = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var a = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var b = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var c = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var d = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            
            var e = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var f = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var g = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var h = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var i = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var j = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var k = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var l = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var m = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var n = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var o = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            var p = new {Total = allForecasts.Count, Forecasts = allForecasts.Skip(from).Take(quantity).ToArray()};
            
            

            return Ok(result);
        }
    }
}
