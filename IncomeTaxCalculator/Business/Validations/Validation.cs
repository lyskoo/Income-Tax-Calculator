namespace Business.Validations;

public static class Validation
{
    public static bool ValidateNumber(decimal? number)
    {
        return number.HasValue && number.Value >= 0;
    }
}
