namespace Business.Handlers;

public interface ITaxHandler
{
    ITaxHandler SetNextHandler(ITaxHandler handler);

    decimal CalculateTax(decimal income);
}
