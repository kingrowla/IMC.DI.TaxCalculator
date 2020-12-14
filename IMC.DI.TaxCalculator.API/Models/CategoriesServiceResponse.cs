using Newtonsoft.Json;
namespace IMC.DI.TaxCalculator.API.Models
{

    public partial class CategoriesServiceResponse
    {
        [JsonProperty("categories")]
        public Category[] Categories { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
