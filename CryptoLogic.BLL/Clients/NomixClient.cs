using CryptoWidget.BLL.Interfaces;
using Repository.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWidget.BLL.Clients
{
    public class NomixClient : IRateRequest
    {
        private HttpClient _client;
        private string _url;

        public NomixClient(string url)
        {
            _client = new HttpClient();
            _url = url;
        }
        public async Task<List<SingleRate>> RequestAsync()
        {
            var response = await _client.GetAsync(_url);
            return new List<SingleRate>();
        }
    }
}
