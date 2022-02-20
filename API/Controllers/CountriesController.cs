using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using CountryInfoService;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class CountriesController : BaseApiController
    {
        private readonly ICountryService countryService;
        private readonly IMapper mapper;
        private readonly ILogger<CountriesController> logger;


        public CountriesController(ICountryService countryService, IMapper mapper, ILogger<CountriesController> logger)
        {
            this.countryService = countryService;
            this.mapper = mapper;
        }
        
        //The purpose of this endpoint is to call a remote service and populate the database tables from the response
        [Authorize]
        [HttpGet("populate")]
        public async Task<ActionResult<bool>> PopulateDataBase()
        {
            CountryInfoServiceSoapTypeClient retClient = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
            try
            {
                Task<CountryNameResponse> result = retClient.CountryNameAsync("MK");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        //The purpose of this endpoint is to return file contents to the client
        [Authorize]
        [HttpGet("{isoCode:string}/flag")]
        public async Task<ActionResult<bool>> GetCountryFlag(string isoCode)
        {
            return true;
        }
    }
}