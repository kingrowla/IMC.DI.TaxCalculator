using IMC.DI.TaxCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.DI.TaxCalculator.API.Mocks
{
    public class CalculateTaxServiceResponseRepository
    {
        public CalculateTaxServiceResponse GetTaxCalculationByZip(string zip)
        {
            CalculateTaxServiceResponse tax = null;
            if (zip == "92093")
            {
                tax = new CalculateTaxServiceResponse()
                {
                   Tax = new Tax()
                   {
                       AmountToCollect = 1.43
                   }
                };
            }
            else if (zip == "32561")
            {

                tax = new CalculateTaxServiceResponse()
                {
                    Tax = new Tax()
                    {
                        AmountToCollect = 1.13
                    }
                };
            }
            else
            {
                //default
            }
            return tax;
        }
    }
}
