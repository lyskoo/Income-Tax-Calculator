using AutoMapper;
using Business.Services;
using Business.Services.Interfaces;
using Data.Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Presentation;

public static class ServicesRegistration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ITaxBandRepository, TaxBandRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITaxBandService, TaxBandService>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(connectionString));

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Mappings.AutoMapper());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
    }
}
