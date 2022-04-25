using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ICurrencyRepository
    {
        void New(Currency currency); 
        Currency? Get(string parentCode, DateTime date); 
        IEnumerable<Currency> GetAll();
        Currency? GetMaxDate(string parentCode); 
        void Edit(Currency currency); 
        void Delete(string parentCode, DateTime date);
    }
}