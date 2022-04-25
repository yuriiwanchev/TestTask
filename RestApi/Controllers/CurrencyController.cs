using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/")]
    [Authorize(AuthenticationSchemes = 
        JwtBearerDefaults.AuthenticationScheme)]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ILogger<CurrencyController> _logger;
        
        public CurrencyController(ICurrencyRepository currencyRepository, ILogger<CurrencyController> logger)
        {
            _currencyRepository = currencyRepository;
            _logger = logger;
        }
        
        [HttpGet("currency/{id}")]
        public ActionResult<Currency> GetCurrency(string id)
        {
            try
            {
                // return _currencyRepository.Get(id, new DateTime(2022,03,02)) switch
                return _currencyRepository.Get(id, DateTime.Today) switch
                {
                    null => _currencyRepository.GetMaxDate(id),
                    var currency => currency
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Error by getting currency with: parent code - {id}, date - {DateTime.Today}",e);
                throw;
            }
            
        }
        
        [HttpGet("currencies")]
        public ActionResult<IReadOnlyCollection<Currency>> GetCurrencies()
        {
            try
            {
                return _currencyRepository.GetAll().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError("Error by getting all currencies",e);
                throw;
            }
        }
    }
}