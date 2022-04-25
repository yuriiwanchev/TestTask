using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ICurrencyDataRepository
    {
        void New(CurrencyData сurrencyData); 
        CurrencyData? Get(string parentCode); 
        IEnumerable<CurrencyData> GetAll();
        void Edit(CurrencyData сurrencyData); 
        void Delete(string parentCode);
    }
}