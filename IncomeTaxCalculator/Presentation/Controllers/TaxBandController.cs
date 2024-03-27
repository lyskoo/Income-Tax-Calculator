using AutoMapper;
using Business.Services.Interfaces;
using Business.Validations;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxBandController : ControllerBase
    {
        private ITaxBandService _taxBandService;
        private IMapper _mapper;

        public TaxBandController(ITaxBandService taxBandService, IMapper mapper)
        {
            _taxBandService = taxBandService;
            _mapper = mapper;
        }

        [HttpGet("salary-info/{income}")]
        public async Task<IActionResult> GetIncomeTax([FromRoute] decimal income)
        {
            if (!Validation.ValidateNumber(income))
            {
                return BadRequest("Invalid income value");
            };

            var salaryInfo = await _taxBandService.GetSalaryInfoAsync(income);
            return Ok(_mapper.Map<SalaryViewModel>(salaryInfo));
        }
    }
}
