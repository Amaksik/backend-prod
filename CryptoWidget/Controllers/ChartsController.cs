using CryptoWidget.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickChart;

namespace CryptoWidget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private IChartService _chartService;
        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;   
        }
        [HttpGet]
        public async Task<IActionResult> GetChart()
        {

            var imageBytes = await _chartService.CreateChart();
            if(imageBytes == null)
            {
                return NotFound();
            }
            return Ok(File(imageBytes, "image/png"));

        }
    }
}
