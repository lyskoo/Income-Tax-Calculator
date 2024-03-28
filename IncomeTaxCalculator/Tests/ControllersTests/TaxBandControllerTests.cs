using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Presentation.ViewModels;

namespace Tests.ControllersTests;

public class TaxBandControllerTests
{
    private readonly Mock<ITaxBandService> _taxBandServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TaxBandController _taxBandController;

    public TaxBandControllerTests()
    {
        _taxBandServiceMock = new Mock<ITaxBandService>();
        _mapperMock = new Mock<IMapper>();
        _taxBandController = new TaxBandController(_taxBandServiceMock.Object, _mapperMock.Object);
    }

    [Theory]
    [InlineData(40000, 11000)]
    [InlineData(10000, 1000)]
    public async Task GetIncomeTax_ValidIncome_ReturnsOk(decimal income, decimal tax)
    {
        var expectedSalaryInfo = new Salary(income, tax);
        var salaryViewModel = new SalaryViewModel()
        {
            GrossAnnualSalary = expectedSalaryInfo.GrossAnnualSalary,
            GrossMonthlySalary = expectedSalaryInfo.GrossMonthlySalary,
            NetAnnualSalary = expectedSalaryInfo.NetAnnualSalary,
            NetMonthlySalary = expectedSalaryInfo.NetMonthlySalary,
            AnnualTaxPaid = expectedSalaryInfo.AnnualTaxPaid,
            MonthlyTaxPaid = expectedSalaryInfo.MonthlyTaxPaid,
        };

        _taxBandServiceMock.Setup(service => service.GetSalaryInfoAsync(income)).ReturnsAsync(expectedSalaryInfo);
        _mapperMock.Setup(mapper => mapper.Map<SalaryViewModel> (expectedSalaryInfo)).Returns(salaryViewModel);

        var result = await _taxBandController.GetIncomeTax(income);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<SalaryViewModel>(okResult.Value);
        Assert.Equal(model.GrossAnnualSalary, expectedSalaryInfo.GrossAnnualSalary);
        Assert.Equal(model.GrossMonthlySalary, expectedSalaryInfo.GrossMonthlySalary);
        Assert.Equal(model.NetMonthlySalary, expectedSalaryInfo.NetMonthlySalary);
        Assert.Equal(model.NetAnnualSalary, expectedSalaryInfo.NetAnnualSalary);
        Assert.Equal(model.AnnualTaxPaid, expectedSalaryInfo.AnnualTaxPaid);
        Assert.Equal(model.MonthlyTaxPaid, expectedSalaryInfo.MonthlyTaxPaid);
    }

    [Theory]
    [InlineData(-10000)]
    [InlineData(-1)]
    public async Task GetIncomeTax_InvalidIncome_ReturnsBadRequest(decimal income)
    {
        var result = await _taxBandController.GetIncomeTax(income);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid income value", badRequestResult.Value);
    }
}
