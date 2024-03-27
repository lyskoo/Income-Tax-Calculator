namespace Business.Models;

public class Salary
{
    private const decimal MonthlyCoefficient = 1m / 12m;

    public decimal GrossAnnualSalary { get; private set; }

    public decimal GrossMonthlySalary { get; private set; }

    public decimal NetAnnualSalary { get; private set; }

    public decimal NetMonthlySalary { get; private set; }

    public decimal AnnualTaxPaid { get; private set; }

    public decimal MonthlyTaxPaid { get; private set; }

    public Salary(decimal grossAnnualSalary, decimal annualTax)
    {
        GrossAnnualSalary = grossAnnualSalary;
        GrossMonthlySalary = grossAnnualSalary * MonthlyCoefficient;
        AnnualTaxPaid = annualTax;
        MonthlyTaxPaid = AnnualTaxPaid * MonthlyCoefficient;
        NetAnnualSalary = grossAnnualSalary - AnnualTaxPaid;
        NetMonthlySalary = GrossMonthlySalary - MonthlyTaxPaid;
    }
}
