using AutoMapper;
using Business.Handlers;
using Business.Models;
using Business.Services.Interfaces;
using Data.UnitOfWork;

namespace Business.Services;

public class TaxBandService : ITaxBandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public TaxBandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Salary> GetSalaryInfoAsync(decimal income)
    {
        var tax = await GetIncomeTaxAsync(income);
        return new Salary(income, tax);
    }

    public async Task<decimal> GetIncomeTaxAsync(decimal income)
    {
        var taxCalculator = await CreateTaxCalculatorAsync();
        return taxCalculator == null
            ? throw new InvalidOperationException("Tax calculator could not be created.")
            : taxCalculator.CalculateTax(income);
    }

    public async Task<List<TaxBandDto>> GetAllTaxBandsAsync()
    {
        return _mapper.Map<IEnumerable<TaxBandDto>>(await _unitOfWork.TaxBands.GetAllAsync()).OrderBy(t => t.LowerLimit).ToList();
    }

    private async Task<TaxCalculator?> CreateTaxCalculatorAsync()
    {
        var taxBands = await GetAllTaxBandsAsync();
        if (!taxBands.Any())
        {
            return null;
        }

        var firstHandler = BuildTaxHandlerChain(taxBands);
        if (firstHandler != null)
        {
            return new TaxCalculator(firstHandler);
        }

        return null;
    }

    private ITaxHandler? BuildTaxHandlerChain(List<TaxBandDto> taxBands)
    {
        ITaxHandler? firstHandler = null;
        ITaxHandler? previousHandler = null;

        foreach (var taxBand in taxBands)
        {
            var handler = new TaxHandler(taxBand);
            if (previousHandler != null)
            {
                previousHandler.SetNextHandler(handler);
            }
            else
            {
                firstHandler = handler;
            }
            previousHandler = handler;
        }

        return firstHandler;
    }
}
