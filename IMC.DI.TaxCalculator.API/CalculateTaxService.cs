using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public class CalculateTaxService
    {
        private readonly HttpClient _httpClient;
        private readonly ICalculateTaxService _calculateTaxService;

        public CalculateTaxService(Uri baseUrl, string token)
        {
            _httpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = baseUrl };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _calculateTaxService = RestService.For<ICalculateTaxService>(_httpClient);
        }

        public async Task<CalculateTaxServiceResponse> GetTaxCalculation(CalculateTaxServiceRequest req)
        {
            return await _calculateTaxService.CalculateTax(req).ConfigureAwait(false);
        }
    }
}
