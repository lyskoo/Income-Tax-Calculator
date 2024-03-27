using Business.Models;
using Data.Entities;

namespace Business.Services.Interfaces;

public interface ITaxBandService
{
    Task<Salary> GetSalaryInfoAsync(decimal income);

    Task<decimal> GetIncomeTaxAsync(decimal income);

    Task<List<TaxBandDto>> GetAllTaxBandsAsync();
}
