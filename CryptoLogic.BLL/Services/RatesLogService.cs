using CryptoWidget.BLL.Interfaces;
using Repository.DAL.Interfaces;
using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Services
{
    public class RatesLogService : IRatesLogService, IRateRequestHandler
    {

        //private readonly ILogger<RatesLogService> _logger;
        private readonly IRatesLogRepository _ratesLogRepository;


        public RatesLogService(/*ILogger<RatesLogService> logger,*/ IRatesLogRepository ratesLogRepository)
        {
            //_logger = logger;
            _ratesLogRepository = ratesLogRepository;
        }
        public async Task<RatesLog> Get(string id)
        {
            var ratelog = await _ratesLogRepository.GetByIdAsync(id);
            return ratelog;
        }

        public async void Post(RatesLog newRatesLog)
        {
            newRatesLog.CreatedDate = DateTime.Now;
            await _ratesLogRepository.CreateNewRatesLogAsync(newRatesLog);
        }

        public async void CreateNewRecord(List<SingleRate> rates)
        {

        }
        public async Task<bool> Put(RatesLog updateRatesLog)
        {
            var people = await _ratesLogRepository.GetByIdAsync(updateRatesLog.Id);
            if (people == null)
            {
                return false;
            }

            await _ratesLogRepository.UpdateRatesLogAsync(updateRatesLog);
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var people = await _ratesLogRepository.GetByIdAsync(id);
            if (people == null)
            {
                return false;
            }

            await _ratesLogRepository.DeleteRatesLogAsync(id);
            return true;
        }

        public async Task<List<SingleRate>> GetCurrentRates()
        {
            var rates = await _ratesLogRepository.GetAllAsync();

            var lastUpdated = rates.OrderByDescending(x => x.CreatedDate).First();

            return lastUpdated.Rates;
        }
    }
}
