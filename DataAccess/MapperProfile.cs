using AutoMapper;
using DataAccess.Models;
using Domain.Models;

namespace DataAccess
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CurrencyDb, Currency>().ReverseMap();
            CreateMap<CurrencyDataDb, CurrencyData>().ReverseMap();
        }
    }
}