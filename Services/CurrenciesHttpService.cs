using CurrenciesDynamicsApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrenciesDynamicsApp.Services
{
    public class CurrenciesHttpService : IHttpService
    {
        private HttpClient httpClient;

        public CurrenciesHttpService(IConfiguration configuration)
        {
            this.httpClient = new HttpClient();
            string baseAddress = configuration.GetValue<string>("httpBaseAddress");
            this.httpClient.BaseAddress = new Uri(baseAddress);
        }

        public async Task<string> GetAsync(DateTime onDate)
        {
            HttpResponseMessage response = await this.httpClient.GetAsync($"?ondate={onDate:yyyy-MM-dd}&periodicity=0");
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
