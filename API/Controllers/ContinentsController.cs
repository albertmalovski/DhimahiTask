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
using Infrastructure.Exceptions;

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

        /***The purpose of this endpoint is to execute 
         * a database query and transform the results before sending them to the client. */

        [Authorize]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 60)]
        [HttpPost("{continentCode}/languages")]
        public async Task<ActionResult<ContinentLangResponse>> GetContinentByCode(string continentCode)
        {
            logger.LogInformation("GET CONTINENT LANGUAGES", continentCode);

            try
            {
                Continent continent = await continentService.GetByCode(continentCode);
                if (continent == null)
                {
                    logger.LogInformation("CONTINENT CODE NOT FOUND", continentCode);
                    return NotFound(new ApiResponse(404, "CONTINENT CODE NOT FOUND:" +continentCode));
                }
                List<Country> countries = await countryService.GetByContinentID(continent.Id);
                List<ContinentLang> languages = new List<ContinentLang>();
                foreach (Country country in countries)
                {
                    foreach (CountryLanguage countryLanguage in country.CountryLanguage)
                    {
                        ContinentLang l = new ContinentLang
                        {
                            Name = countryLanguage.Language.Name,
                            Code = countryLanguage.Language.ISOCode,
                        };
                        bool ADDLang = true;
                        foreach(ContinentLang CL in languages)
                        {
                            if(CL.Code == l.Code)
                            {
                                ADDLang = false;
                                break;
                            }
                        }
                        if (ADDLang) languages.Add(l);
                    }
                }
                logger.LogInformation("SUCCESSFULLY GET CONTINENT LANGUAGES", continentCode);
                return new ContinentLangResponse
                {
                    languages = languages
                };
            }
            catch (DBException ex)
            {
                logger.LogError("DATABASE ERROR", ex.Message);
                return BadRequest(new ApiResponse(500, "SERVER INTERNAL ERROR"));
            }
            catch (GeneralException ex)
            {
                logger.LogError("SERVICE ERROR", ex.Message);
                return BadRequest(new ApiResponse(500, "SERVER INTERNAL ERROR"));
            }
            catch (Exception ex)
            {
                logger.LogError("SERVER INTERNAL ERROR", ex.Message);
                return BadRequest(new ApiResponse(500, "SERVER INTERNAL ERROR"));
            }
        }
    }
}