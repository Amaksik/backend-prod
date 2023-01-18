using CryptoWidget.BLL.ClientsConfiguration;
using CryptoWidget.BLL.Interfaces;
using CryptoWidget.BLL.Models;
using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.IO;

namespace CryptoWidget.BLL.Clients
{
    public class CoinLayerClient : IRateRequest
    {
        private HttpClient _client;
        private string _url;
        private string _key;

        public CoinLayerClient(IOptions<CoinLayerSettings> options)
        {
            _client = new HttpClient();
            _url = options.Value.BaseURl;
            _key = options.Value.Key;
        }
        public async Task<List<SingleRate>> RequestAsync()
        {
            var response = await _client.GetAsync(_url+ "live?access_key=" + _key);

            var customerJsonString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CoinLayerWrapper>(customerJsonString);

            return formatResponse(result);
        }
        private List<SingleRate> formatResponse(CoinLayerWrapper response)
        {
            var result = new List<SingleRate>();


            foreach (PropertyInfo prop in response.rates.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (type == typeof(double))
                {
                    var item = new SingleRate()
                    {
                        CurrencyName = prop.Name,
                        Rate = Convert.ToDouble(prop.GetValue(response.rates))
                    };

                    result.Add(item);
                }
            }

            return result;
        }

    }
}
