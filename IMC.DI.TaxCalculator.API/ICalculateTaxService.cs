using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public interface ICalculateTaxService
    {
        [Post("/taxes")]
        Task<CalculateTaxServiceResponse> CalculateTax([Body] CalculateTaxServiceRequest request);
    }
}
