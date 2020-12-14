using IMC.DI.TaxCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.DI.TaxCalculator.API.Mocks
{
    public class CalculateTaxServiceRequestRepository : ICalculateTaxServiceRequestRepository
    {
        public CalculateTaxServiceRequest GetTaxRequestByZip(string zip)
        {
            CalculateTaxServiceRequest tax = null;
            if (zip == "92093")
            {
                tax = new CalculateTaxServiceRequest (){
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",
                Amount = 15,
                Shipping = 2,
                NexusAddresses = new[] {
                    new NexusAddress {
                        Id = "Main Location",
                        Country = "US",
                        Zip = "92093",
                        State = "CA",
                        City = "La Jolla",
                        Street = "9500 Gilman Drive"
                    }
                },
                LineItems = new[] {
                    new LineItem {
                        Id = "1",
                        Quantity = "1",
                        ProductTaxCode = "20010",
                        UnitPrice = 15,
                        Discount = 0
                    }
                }
            }; 
            }
            else if (zip == "32561")
            {
                tax = new CalculateTaxServiceRequest()
                {
                    FromCountry = "US",
                    FromZip = "32561",
                    FromState = "FL",
                    FromCity = "Pensacola",
                    FromStreet = "9500 Gilman Drive",
                    ToCountry = "US",
                    ToZip = "32561",
                    ToState = "FL",
                    ToCity = "Pensacola",
                    ToStreet = "1335 E 103rd St",
                    Amount = 15,
                    Shipping = 2,
                    NexusAddresses = new[] {
                    new NexusAddress {
                        Id = "Main Location",
                        Country = "US",
                        Zip = "32561",
                        State = "FL",
                        City = "Pensacola",
                        Street = "9500 Gilman Drive"
                    }
                },
                    LineItems = new[] {
                    new LineItem {
                        Id = "1",
                        Quantity = "1",
                        ProductTaxCode = "20010",
                        UnitPrice = 15,
                        Discount = 0
                    }
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
