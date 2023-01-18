using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL.Interfaces
{
    public interface IRatesLogRepository
    {
        Task<List<RatesLog>> GetAllAsync();
        Task<RatesLog> GetByIdAsync(string id);
        Task CreateNewRatesLogAsync(RatesLog newRatesLog);
        Task UpdateRatesLogAsync(RatesLog ratesLogToUpdate);
        Task DeleteRatesLogAsync(string id);
    }
}
