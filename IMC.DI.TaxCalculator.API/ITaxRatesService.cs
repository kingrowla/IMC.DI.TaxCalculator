using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public interface ITaxRatesService
    {
        [Get("/rates/{zip}")]
        Task<TaxRatesServiceResponse> GetTaxRates(string zip);
    }
}

