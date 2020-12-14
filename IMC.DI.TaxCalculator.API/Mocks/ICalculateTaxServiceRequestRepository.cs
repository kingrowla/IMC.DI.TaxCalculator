using IMC.DI.TaxCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.DI.TaxCalculator.API.Mocks
{
    public interface ICalculateTaxServiceRequestRepository
    {
        CalculateTaxServiceRequest GetTaxRequestByZip(string zip);
    }
}
