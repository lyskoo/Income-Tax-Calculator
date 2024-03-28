using Business.Handlers;
using Business.Models;

namespace Tests.BusinessTests;

public class TaxHandlerTests
{
    [Theory]
    [InlineData(0, 10000, 10, 500, 5000)]
    [InlineData(10000, null, 20, 3000, 15000)]
    public void CalculateTax_CalculatesTax(int lowerLimit, int? upperLimit, int taxRate, decimal expectedTax, decimal income)
    {
        var taxBand = new TaxBandDto
        {
            LowerLimit = lowerLimit,
            UpperLimit = upperLimit,
            TaxRate = taxRate
        };
        var handler = new TaxHandler(taxBand);
        var tax = handler.CalculateTax(income);

        Assert.Equal(expectedTax, tax);
    }


    [Theory]
    [InlineData(0, 5000, 0, 5000, 20000, 20, 10000, 1000)]
    public void CalculateTax_WithNextHandler_CalculatesTax(int lowerLimit1, int? upperLimit1, int taxRate1, int lowerLimit2, int? upperLimit2, int taxRate2, decimal income, decimal expectedTax)
    {
        var taxBand1 = new TaxBandDto
        {
            LowerLimit = lowerLimit1,
            UpperLimit = upperLimit1,
            TaxRate = taxRate1
        };
        var taxBand2 = new TaxBandDto
        {
            LowerLimit = lowerLimit2,
            UpperLimit = upperLimit2,
            TaxRate = taxRate2
        };

        var handler1 = new TaxHandler(taxBand1);
        var handler2 = new TaxHandler(taxBand2);
        handler1.SetNextHandler(handler2);

        var tax = handler1.CalculateTax(income);

        Assert.Equal(expectedTax, tax);
    }
}
