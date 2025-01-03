using ColtonsApp.DatabaseContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColtonsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET api/weatherforecast
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            var result = await _context.WeatherForecasts.ToListAsync();
            return Ok(result);
        }

        // POST api/weatherforecast
        [HttpPost()]
        public async Task<ActionResult> AddWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {
            // need ensure id is unique

            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
