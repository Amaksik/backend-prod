using CryptoWidget.BLL.Interfaces;
using Repository.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Services
{
    public class RateService : IRateService
    {
        private IRateRequestHandler _clientWrapper;
        private IRatesLogService _ratesLogService;
        public RateService ( IRateRequestHandler clietWrapper, IRatesLogService ratesLogService)
        {
            _clientWrapper = clietWrapper;
            _ratesLogService = ratesLogService;
        }

        public async Task<List<string>> GetAllowedNames()
        {
            var globalRates = await _clientWrapper.GetCurrentRates();

            return globalRates.Select(x => x.CurrencyName).ToList();
        }

        public async Task<List<SingleRate>> GetFilteredRates(List<string> requestedCurrencies)
        {
            var globalRates = await _clientWrapper.GetCurrentRates();

            _ratesLogService.CreateNewRecord(globalRates);

            return globalRates
                               .Where(t => requestedCurrencies.Contains(t.CurrencyName))
                               .ToList();
        }
    }
}
