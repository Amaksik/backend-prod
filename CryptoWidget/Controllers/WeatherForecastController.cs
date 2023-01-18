using Microsoft.AspNetCore.Mvc;
using Repository.DAL.Interfaces;

namespace CryptoWidget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRatesLogRepository _ratesLogRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRatesLogRepository ratesLogRepository)
        {
            _logger = logger;
            _ratesLogRepository = ratesLogRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get()
        {
            var people = await _ratesLogRepository.GetAllAsync();
            return Ok(people);

        }
    }
}