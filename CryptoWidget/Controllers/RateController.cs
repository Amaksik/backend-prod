using CryptoWidget.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL.Interfaces;

namespace CryptoWidget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {

        private readonly ILogger<RateController> _logger;
        private readonly IRateService _rateService;

        public RateController(ILogger<RateController> logger, IRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNames()
        {
            var names = await _rateService.GetAllowedNames();
            if (names == null || names.Count() == 0)
            {
                return NotFound();
            }

            return Ok(names);
        }


        [HttpPost]
        public async Task<IActionResult> GetFiltered(List<string> requestedCurrencies)
        {
            var rates = await _rateService.GetFilteredRates(requestedCurrencies);
            if (rates == null || rates.Count() == 0)
            {
                return NotFound();
            }

            return Ok(rates);
        }
        }
}
