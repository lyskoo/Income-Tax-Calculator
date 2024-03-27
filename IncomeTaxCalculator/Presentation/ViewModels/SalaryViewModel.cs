namespace Presentation.ViewModels;

public class SalaryViewModel
{
    public decimal GrossAnnualSalary { get; private set; }

    public decimal GrossMonthlySalary { get; private set; }

    public decimal NetAnnualSalary { get; private set; }

    public decimal NetMonthlySalary { get; private set; }

    public decimal AnnualTaxPaid { get; private set; }

    public decimal MonthlyTaxPaid { get; private set; }
}
