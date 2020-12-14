using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoriesService _catergoriesService;

        public CategoriesService(Uri baseUrl, string token)
        {
            _httpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = baseUrl };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _catergoriesService = RestService.For<ICategoriesService>(_httpClient);
        }

        public async Task<CategoriesServiceResponse> GetCategories()
        {
            return await _catergoriesService.GetCategories().ConfigureAwait(false);
        }
    }
}
