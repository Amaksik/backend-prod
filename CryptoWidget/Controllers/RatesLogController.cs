using Microsoft.AspNetCore.Mvc;
using Repository.DAL.Interfaces;
using Repository.DAL.Models;

namespace CryptoWidget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesLogController : ControllerBase
    {
        private readonly ILogger<RatesLogController> _logger;
        private readonly IRatesLogRepository _ratesLogRepository;

        public RatesLogController(ILogger<RatesLogController> logger, IRatesLogRepository ratesLogRepository)
        {
            _logger = logger;
            _ratesLogRepository = ratesLogRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string chislo = id;
            var people = await _ratesLogRepository.GetByIdAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RatesLog newRatesLog)
        {
            newRatesLog.CreatedDate = DateTime.Now;
            await _ratesLogRepository.CreateNewRatesLogAsync(newRatesLog);
            return CreatedAtAction(nameof(Get), new { id = newRatesLog.Id }, newRatesLog);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RatesLog updateRatesLog)
        {
            var people = await _ratesLogRepository.GetByIdAsync(updateRatesLog.Id);
            if (people == null)
            {
                return NotFound();
            }

            await _ratesLogRepository.UpdateRatesLogAsync(updateRatesLog);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var people = await _ratesLogRepository.GetByIdAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            await _ratesLogRepository.DeleteRatesLogAsync(id);
            return NoContent();
        }
    }
}
