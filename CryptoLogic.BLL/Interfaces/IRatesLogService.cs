using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Interfaces
{
    public interface IRatesLogService
    {
        Task<RatesLog> Get(string id);

        void Post(RatesLog newRatesLog);

        Task<bool> Put(RatesLog updateRatesLog);
        Task<bool> Delete(string id);
    }
}
