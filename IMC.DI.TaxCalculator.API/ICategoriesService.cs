using IMC.DI.TaxCalculator.API.Models;
using Refit;
using System.Threading.Tasks;

namespace IMC.DI.TaxCalculator.API
{
    public interface ICategoriesService
    {
        [Get("/categories")]
        Task<CategoriesServiceResponse> GetCategories();
    }
}
