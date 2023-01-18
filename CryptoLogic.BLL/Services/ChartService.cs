using CryptoWidget.BLL.Interfaces;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using QuickChart;
using Repository.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Services
{
    public class ChartService : IChartService
    {

        private readonly IRatesLogRepository _ratesLogRepository;
        public ChartService(IRatesLogRepository ratesLogRepository)
        {
            _ratesLogRepository = ratesLogRepository;
        }
        public async Task<byte[]> CreateChart()
        {
            Chart qc = new Chart();

			var rates = await _ratesLogRepository.GetAllAsync();

            var prices = rates.Select(x => x.Rates).ToList();

            var dates = rates.Select(x => x.CreatedDate).ToList();

            qc.Width = 500;
            qc.Height = 300;

            qc.Config = @"{
						  type: 'line',
						  data: {
						    labels: [10,12,14,16,18],
						    datasets: [{
						      label: 'BTC',
						      data: [ 19500, 19650, 19800, 19400, 19450 ],
						      fill: false,
						      borderColor: 'green',
						      backgroundColor: 'green',
						    }]
						  },
						  options: {
						    plugins: {
						      datalabels: {
						        display: true,
						        align: 'bottom',
						        backgroundColor: '#ccc',
						        borderRadius: 3
						      },
						    }
						  }
						}";
            byte[] imageBytes = qc.ToByteArray();

			return imageBytes;	
        }
    }
}
