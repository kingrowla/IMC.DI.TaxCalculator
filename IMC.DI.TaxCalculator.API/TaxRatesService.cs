using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public class TaxRatesService
    {
        private readonly HttpClient _httpClient;
        private readonly ITaxRatesService _taxRatesService;

        public TaxRatesService(Uri baseUrl, string token)
        {
            _httpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = baseUrl };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _taxRatesService = RestService.For<ITaxRatesService>(_httpClient);
        }

        public async Task<TaxRatesServiceResponse> GetTaxRates(string zip)
        {
            return await _taxRatesService.GetTaxRates(zip).ConfigureAwait(false);
        }
    }
}
