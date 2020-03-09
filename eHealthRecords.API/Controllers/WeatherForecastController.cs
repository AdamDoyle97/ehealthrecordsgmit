using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eHealthRecords.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eHealthRecords.API.Controllers
{
    //http//localhost:5000/api (default it goes to port 5000)
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // injects DataContext into WeatherController
        // private field _context can be used throughout class
        private readonly DataContext _context;
        public WeatherForecastController(DataContext context) 
        {
            _context = context;

        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // get api/weatherforecast values
        [AllowAnonymous]
        [HttpGet]
        // IActionResult allows hhtp responses to the client
        // Async allows other requests to be processed and not be haulted by one processing
        public async Task<IActionResult> GetWeatherForecast() //async code is used for when more than one user is requesting
        {
            // ToList gets values as a list and sends them out to client (postman for testing)
            var values = await _context.Values.ToListAsync();

            return Ok(values); // if successful returns http 200Ok to postman
        }
    }
}
