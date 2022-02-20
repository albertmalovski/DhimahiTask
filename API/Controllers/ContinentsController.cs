using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using API.Errors;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Controllers
{
    public class ContinentsController : BaseApiController
    {
        private readonly IContinentService continentService;
        private readonly ICountryService countryService;
        private readonly IMapper mapper;
        private readonly ILogger<ContinentsController> logger;

        public ContinentsController(IContinentService continentService, ICountryService countryService,
                                    IMapper mapper, ILogger<ContinentsController> logger)
        {
            this.continentService = continentService;
            this.countryService = countryService;
            this.mapper = mapper;
            this.logger = logger;
        }

        //The purpose of this endpoint is to execute a database query and transform the results before sending them to the client. 
        //[Authorize]
        [HttpPost("{continentCode}/languages")]
        public async Task<ActionResult<bool>> GetContinentByCode(string continentCode)
        {
            Continent continent = await continentService.GetByCode(continentCode);
            if (continent == null) return BadRequest(new ApiResponse(404));
            List<Country> countries = await countryService.GetByContinentID(continent.Id);
            List<Language> languages = new List<Language>();
            foreach(Country country in countries)
            {
                foreach (CountryLanguage countryLanguage in country.CountryLanguage)
                {
                    if(!languages.Contains(countryLanguage.Language))
                        languages.Add(countryLanguage.Language);
                }
            }
            logger.LogInformation("Payment Succeeded");
            logger.LogInformation("Order updated to payment received: ");
            logger.LogInformation("Payment failed: ",1);
            logger.LogInformation("Payment failed: ", 1);
            return true;
        }
    }
}