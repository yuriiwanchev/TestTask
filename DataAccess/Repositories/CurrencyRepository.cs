using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Models;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyDbContext context;
        private readonly IMapper mapper;
        
        public CurrencyRepository(CurrencyDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }
        
        public void New(Currency currency)
        {
            var itemDb = mapper.Map<CurrencyDb>(currency);
            var result = context.CurrencyDbs.Add(itemDb);
            context.SaveChanges();
        }

        public Currency? Get(string parentCode, DateTime date)
        {
            var itemDb = context.CurrencyDbs.FirstOrDefault(x => 
                x.ParentCode == parentCode &&
                x.Date == date);
            return mapper.Map<Currency?>(itemDb);
        }

        public Currency? GetMaxDate(string parentCode)
        {
            DateTime maxDate = context.CurrencyDbs.Max(itemDb => itemDb.Date);
            var itemDb = context.CurrencyDbs.FirstOrDefault(x => 
                x.ParentCode == parentCode &&
                x.Date == maxDate);
            return mapper.Map<Currency?>(itemDb);
        }

        public IEnumerable<Currency> GetAll()
        {
            var itemsDb = context.CurrencyDbs.ToList();
            return mapper.Map<IReadOnlyCollection<Currency>>(itemsDb);
        }

        public void Edit(Currency currency)
        {
            if (context.CurrencyDbs.Find(currency.ParentCode, currency.Date) is CurrencyDb currencyInDb)
            {
                currencyInDb.ParentCode = currency.ParentCode;
                currencyInDb.Date = currency.Date;
                currencyInDb.CurrencyValue = currency.CurrencyValue;
                context.Entry(currencyInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no that currency");
            }
        }

        public void Delete(string parentCode, DateTime date)
        {
            var itemToDelete = context.CurrencyDbs.Find(parentCode, date);
            context.Entry(itemToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}