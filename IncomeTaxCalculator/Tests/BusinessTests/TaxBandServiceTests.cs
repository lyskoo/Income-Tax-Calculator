using AutoMapper;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.UnitOfWork;
using Moq;

namespace Tests.BusinessTests;

public class TaxBandServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TaxBandService _salaryService;
    private List<TaxBand> taxBandEntities;
    private List<TaxBandDto> taxBandDtos;


    public TaxBandServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _salaryService = new TaxBandService(_unitOfWorkMock.Object, _mapperMock.Object);

        taxBandEntities = new List<TaxBand>
        {
            new TaxBand { Name = "A", LowerLimit = 10000, UpperLimit = 20000, TaxRate = 0 },
            new TaxBand { Name = "B", LowerLimit = 20000, UpperLimit = 30000, TaxRate = 10 },
            new TaxBand { Name = "C", LowerLimit = 30000, UpperLimit = 40000, TaxRate = 20 }
        };
        _unitOfWorkMock.Setup(uow => uow.TaxBands.GetAllAsync()).ReturnsAsync(taxBandEntities);

        taxBandDtos = new List<TaxBandDto>()
        {
            new TaxBandDto { Name = "A", LowerLimit = 10000, UpperLimit = 20000, TaxRate = 0 },
            new TaxBandDto { Name = "B", LowerLimit = 20000, UpperLimit = 30000, TaxRate = 10 },
            new TaxBandDto { Name = "C", LowerLimit = 30000, UpperLimit = 40000, TaxRate = 20 }
        };
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<TaxBandDto>>(taxBandEntities)).Returns(taxBandDtos);
    }

    [Fact]
    public async Task GetAllTaxBandsAsync_ReturnsMappedTaxBands()
    {
        var result = await _salaryService.GetAllTaxBandsAsync();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<TaxBandDto>>(result);
        Assert.Equal(taxBandEntities.Count, result.Count);
    }

    [Fact]
    public async Task GetAllTaxBandsAsync_ReturnsOrderedTaxBands()
    {
        var result = await _salaryService.GetAllTaxBandsAsync();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<TaxBandDto>>(result);
        Assert.Equal(taxBandEntities.Count, result.Count);
        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].LowerLimit <= result[i + 1].LowerLimit);
        }
    }

    [Fact]
    public async Task GetSalaryInfoAsync_ValidIncome_ReturnsSalaryObject()
    {
        decimal income = 50000;

        var result = await _salaryService.GetSalaryInfoAsync(income);

        Assert.IsType<Salary>(result);
        Assert.Equal(income, result.GrossAnnualSalary);
        Assert.True(result.AnnualTaxPaid > 0);
    }

    [Theory]
    [InlineData(50000)]
    [InlineData(10000)]
    [InlineData(10)]
    public async Task GetIncomeTaxAsync_ValidIncome_ReturnsTax(decimal income)
    {
        var result = await _salaryService.GetIncomeTaxAsync(income);

        Assert.IsType<decimal>(result);
        Assert.True(result >= 0);
    }

    [Theory]
    [InlineData(-50000)]
    [InlineData(-1)]
    [InlineData(-10)]
    public async Task GetIncomeTaxAsync_InvalidIncome_ThrowsInvalidOperationException(decimal income)
    {
        _unitOfWorkMock.Setup(uow => uow.TaxBands.GetAllAsync()).ReturnsAsync(new List<TaxBand>());
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<TaxBandDto>>(It.IsAny<List<TaxBand>>())).Returns(new List<TaxBandDto>());

        await Assert.ThrowsAsync<InvalidOperationException>(() => _salaryService.GetIncomeTaxAsync(income));
    }
}
