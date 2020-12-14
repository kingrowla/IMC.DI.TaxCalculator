using FluentAssertions;
using IMC.DI.TaxCalculator.API;
using IMC.DI.TaxCalculator.API.Mocks;
using IMC.DI.TaxCalculator.API.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IMC.DI.TaxCalculator.Tests
{
    public class CalculateTaxServiceTest
    {

        private readonly Uri baseURI = new Uri("https://api.taxjar.com/v2");
        private readonly string APIKey = "5da2f821eee4035db4771edab942a4cc";

        [Fact]
        public async Task GetAllCategories()
        {
           var client = new CategoriesService(baseURI, APIKey);
           var result = await client.GetCategories();

           Assert.NotNull(result);

        }

        [Fact]
        public async Task CalculateTaxForZip()
        {
            //amount based on CA 92093//
            var expectedAmountToCollect = 1.43;
            var service = new CalculateTaxService(baseURI, APIKey);
            var reqRepository = new CalculateTaxServiceRequestRepository();
            var request = reqRepository.GetTaxRequestByZip("92093");
            var result = await service.GetTaxCalculation(request);

            result.Should().BeOfType<CalculateTaxServiceResponse>();
            result.Tax.AmountToCollect.Should().Be(expectedAmountToCollect);
        }

        [Fact]
        public async Task CalculateIncorrectTaxForZip()
        {
            //amount based on CA 92093 With FL repo zip
            var expectedAmountToCollect = 1.43;
            var service = new CalculateTaxService(baseURI, APIKey);
            var reqRepository = new CalculateTaxServiceRequestRepository();
            var request = reqRepository.GetTaxRequestByZip("32561");
            var result = await service.GetTaxCalculation(request);

            result.Should().BeOfType<CalculateTaxServiceResponse>();
            result.Tax.AmountToCollect.Should().NotBe(expectedAmountToCollect);
        }
    }
}
