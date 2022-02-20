using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using CountryInfoService;
using API.Errors;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class ContinentsController : BaseApiController
    {
        private readonly IContinentService continentService;
        private readonly IMapper mapper;
        private readonly ILogger<ContinentsController> logger;

        public ContinentsController(IContinentService continentService, IMapper mapper, ILogger<ContinentsController> logger)
        {
            this.continentService = continentService;
            this.mapper = mapper;
            this.logger = logger;
        }

        //The purpose of this endpoint is to execute a database query and transform the results before sending them to the client. 
        [Authorize]
        [HttpGet("{continentCode:string}/languages")]
        public async Task<ActionResult<bool>> GetContinentByCode(string continentCode)
        {
            Task<Continent> continent = continentService.GetByCode(continentCode);
            if (continent == null) return BadRequest(new ApiResponse(404));
                    logger.LogInformation("Payment Succeeded");
                    logger.LogInformation("Order updated to payment received: ");
                    logger.LogInformation("Payment failed: ",1);
                    logger.LogInformation("Payment failed: ", 1);
            return true;
        }
    }
}