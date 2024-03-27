namespace Business.Handlers;

public class TaxCalculator
{
    private readonly ITaxHandler _handler;

    public TaxCalculator(ITaxHandler handler)
    {
        _handler = handler;
    }

    public decimal CalculateTax(decimal income)
    {
        return _handler.CalculateTax(income);
    }
}
