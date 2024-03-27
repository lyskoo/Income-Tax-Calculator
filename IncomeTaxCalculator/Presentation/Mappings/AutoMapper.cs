using AutoMapper;
using Business.Models;
using Data.Entities;
using Presentation.ViewModels;

namespace Presentation.Mappings;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<TaxBand, TaxBandDto>().ReverseMap();
        CreateMap<Salary, SalaryViewModel>().ReverseMap();
    }
}
