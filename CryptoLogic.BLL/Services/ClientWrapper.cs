using CryptoWidget.BLL.Interfaces;
using Repository.DAL.Interfaces;
using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Services
{
    public class ClientWrapper : IRateRequestHandler
    {
        private readonly IRatesLogRepository _ratesLogRepository;
        private readonly IRateRequest _apiClient;
        private readonly IRateRequest _additiionalClient;
        public ClientWrapper(IRatesLogRepository ratesLogRepository, IRateRequest firstClient, IRateRequest secondClient)
        {
            _ratesLogRepository = ratesLogRepository;
            _apiClient = firstClient;
            _additiionalClient = secondClient;

        }

        public async Task<List<SingleRate>> GetCurrentRates()
        {
            return await ChainOfResponsibility_Get();
        }
        private async Task<List<SingleRate>> ChainOfResponsibility_Get()
        {
            var rates = await _apiClient.RequestAsync();
            if (rates == null)
            {
                rates = await _additiionalClient.RequestAsync();
                
            }
            if (rates == null)
            {
                var allRecords = await _ratesLogRepository.GetAllAsync();
                var latestRates = allRecords.AsQueryable().OrderByDescending(p => p.CreatedDate).First();

                rates = latestRates.Rates;

            }
            return rates;

        }
    }
}
