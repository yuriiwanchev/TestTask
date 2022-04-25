using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataAccess.Repositories;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public static class Bootstapper
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            return services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<CurrencyDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<ICurrencyRepository, CurrencyRepository>()
                .AddScoped<ICurrencyDataRepository, CurrencyDataRepository>();
        }

        public static string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("dbconnectionsettings.json");
                
            IConfigurationRoot configuration = builder.Build();

            return configuration.GetConnectionString("CurrencyDb");
        }
    }
}