using Business.Models;

namespace Business.Handlers;

public class TaxHandler : ITaxHandler
{
    private readonly TaxBandDto _taxBand;
    private ITaxHandler _nextHandler;

    public TaxHandler(TaxBandDto taxBand)
    {
        _taxBand = taxBand;
    }

    public ITaxHandler SetNextHandler(ITaxHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public decimal CalculateTax(decimal income)
    {
        decimal totalTax = 0;

        if (_taxBand.UpperLimit != null)
        {
            decimal taxableIncome = Math.Min((decimal)(_taxBand.UpperLimit - _taxBand.LowerLimit), income);
            totalTax += taxableIncome * _taxBand.TaxRate / 100;
            income -= taxableIncome;

            if (income <= 0)
            {
                return totalTax;
            }
        }
        else
        {
            totalTax += income * _taxBand.TaxRate / 100;
        }

        if (_nextHandler != null)
        {
            totalTax += _nextHandler.CalculateTax(income);
        }

        return totalTax;
    }
}
