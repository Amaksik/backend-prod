using MongoDB.Driver;
using Repository.DAL.Interfaces;
using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL.Repositories
{
    public class RatesLogRepository : IRatesLogRepository
    {
        private readonly IMongoCollection<RatesLog> _ratesLogCollection;

        public RatesLogRepository(IMongoDatabase mongoDatabase)
        {
            _ratesLogCollection = mongoDatabase.GetCollection<RatesLog>("rateslog");
        }
        public async Task<List<RatesLog>> GetAllAsync()
        {
            return await _ratesLogCollection.Find(_ => true).ToListAsync();
        }
        public async Task<RatesLog> GetByIdAsync(string id)
        {
            return await _ratesLogCollection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateNewRatesLogAsync(RatesLog newRatesLog)
        {
            await _ratesLogCollection.InsertOneAsync(newRatesLog);
        }
        public async Task UpdateRatesLogAsync(RatesLog ratesLogToUpdate)
        {
            await _ratesLogCollection.ReplaceOneAsync(x => x.Id == ratesLogToUpdate.Id, ratesLogToUpdate);
        }
        public async Task DeleteRatesLogAsync(string id)
        {
            await _ratesLogCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
