using System;
using ConsoleApp.Services;
using DataAccess;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using CurrencyLib = ConsoleApp.Services.CurrencyLib;

namespace ConsoleApp
{
    internal class Program
    {
        // Number of days to take currencies from today
        const int N = 3;
        
        static void Main(string[] args)
        {
            // setup our DI
            var serviceProvider = new ServiceCollection()
                .AddDataAccess(Bootstapper.GetConnectionString())
                .BuildServiceProvider();
            
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
           
            Log.Information("Starting up");

            
            var currencyDataRepository = serviceProvider.GetService<ICurrencyDataRepository>();
            var currencyLib = new CurrencyLib();

            // fill database with data about currencies
            // if no that data, add
            // if taken data is already there, do nothing
            foreach (var currency in currencyLib.GetArrayXmlElements())
            {
                var currencyFromDb = currencyDataRepository.Get(currency.ParentCode);
                if ( currencyFromDb is not null)
                {
                    if (!currency.Equals(currencyFromDb))
                    {
                        currencyDataRepository.Edit(currency);
                    }
                }
                else
                {
                    currencyDataRepository.New(currency);
                }
            }
            Log.Information("The database is filled with currency data");
            
            
            var currencyRepository = serviceProvider.GetService<ICurrencyRepository>();
            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            
            // fill database with currencies at dates
            // if no that data, add
            // if taken data is already there, do nothing
            for (int i = N; i > 0; i--)
            {
                var currencyLog = new CurrencyLog(date);
                foreach (var currency in currencyLog.GetArrayXmlElements())
                {
                    var currencyFromDb = currencyRepository.Get(currency.ParentCode, currency.Date); 
                    if ( currencyFromDb is not null)
                    {
                        if (!currency.Equals(currencyFromDb))
                        {
                            currencyRepository.Edit(currency);
                        }
                    }
                    else
                    {
                        currencyRepository.New(currency);
                    }
                }

                Log.Information($"The database is filled with currencies at {date}");
                date = date.AddDays(-1);
            }
            
            Log.Information("All done!");
            Log.CloseAndFlush();
        }
    }
}