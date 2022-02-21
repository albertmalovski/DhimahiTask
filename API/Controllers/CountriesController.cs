using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Net;
using API.Errors;
using System.Xml.Linq;
using System.Collections.Generic;
using API.ReferencedServices;
using Infrastructure.Exceptions;
using API.Helpers;

namespace API.Controllers
{
    public class CountriesController : BaseApiController
    {
        private readonly ICountryService countryService;
        private readonly IContinentService continentService;
        private readonly ILanguageService languageService;
        private readonly ICurrencyService currencyService;
        private readonly IMapper mapper;
        private readonly ILogger<CountriesController> logger;
        private readonly string ImagesLocation = "wwwroot/images/flags/";

        public CountriesController(ICountryService countryService, IContinentService continentService, ICurrencyService currencyService, 
            ILanguageService languageService, IMapper mapper, ILogger<CountriesController> logger)
        {
            this.countryService = countryService;
            this.continentService = continentService;
            this.currencyService = currencyService;
            this.languageService = languageService;
            this.mapper = mapper;
            this.logger = logger;
        }
        
        //The purpose of this endpoint is to call a remote service and populate the database tables from the response
        [Authorize]
        [HttpGet("populate")]
        public async Task<ActionResult<PopulateResponse>> PopulateDataBase()
        {
            try
            {
                logger.LogInformation("STARTING POPULATE DATABASE WITH DATA FROM WEB SERVICE");
                XNamespace ns = CountryInfoService.xNamespace;

                IEnumerable<XElement> result = CountryInfoService.GetCountryInfoService();
                foreach (XElement element in result)
                {
                    string sISOCode = (string)element.Element(ns + "sISOCode");
                    string sName = (string)element.Element(ns + "sName");
                    string sCapitalCity = (string)element.Element(ns + "sCapitalCity");
                    string sPhoneCode = (string)element.Element(ns + "sPhoneCode");
                    string sContinentCode = (string)element.Element(ns + "sContinentCode");
                    string sCurrencyISOCode = (string)element.Element(ns + "sCurrencyISOCode");
                    string sCountryFlag = (string)element.Element(ns + "sCountryFlag");

                    int ContinentID = await (continentService.CreateOrUpdate(sContinentCode, sContinentCode));
                    int CurrencyID = await (currencyService.CreateOrUpdate(sCurrencyISOCode, sCurrencyISOCode));
                    int CountryID = await (countryService.CreateOrUpdate(sISOCode, sName, sCapitalCity, sPhoneCode, sCountryFlag, ContinentID, CurrencyID));

                    IEnumerable<XElement> languages = element.Descendants(ns + "tLanguage");
                    List<int> langIDs = new List<int>();
                    foreach (XElement lang in languages)
                    {
                        string langISOCode = (string)lang.Element(ns + "sISOCode");
                        string langName = (string)lang.Element(ns + "sName");
                        int languageID = await (languageService.CreateOrUpdate(langISOCode, langName));
                        langIDs.Add(languageID);
                    }

                    int InsertedLang = await (countryService.CreateOrUpdateLang(CountryID, langIDs));
                }

            }
            catch (WebException ex)
            {
                logger.LogError("WEB SERVICE EXCEPTION ERROR", ex.Message);
                return BadRequest(new ApiResponse(500, "WEB SERVICE INTERNAL ERROR"));
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

            logger.LogInformation("SUCCESSFULLY POPULATED DATABASE WITH DATA FROM WEB SERVICE");
            return new PopulateResponse
            {
                Status = "SUCCESSFULLY",
                Message = "SUCCESSFULLY POPULATED DATABASE WITH DATA FROM WEB SERVICE",
            }; 
        }
       

        //The purpose of this endpoint is to return file contents to the client
        [Authorize]
        [HttpPost("{isoCode}/flag")]
        public async Task<ActionResult<CountryFlagResponse>> GetCountryFlag(string isoCode)
        {
            logger.LogInformation("GET COUNTRY FLAG LANGUAGES", isoCode);

            try
            {
                string ImageName = ImagesLocation + isoCode + ".jpg";
                Country country = await (countryService.GetByCode(isoCode));
                if (country == null)
                {
                    logger.LogInformation("COUNTRY CODE NOT FOUND", isoCode);
                    return NotFound(new ApiResponse(404, "CONTINENT CODE NOT FOUND:" + isoCode));
                }
             
                string URL = country.CountryFlag;
                bool SuccessDownloaded = true;
                if (!ImageHellper.checkIsDownloaded(ImageName))
                {
                    SuccessDownloaded = ImageHellper.DownloadImageSafe(URL, ImageName);
                    logger.LogInformation("DOWNLOADED COUNTRY FLAG", isoCode);
                }

                if (!SuccessDownloaded) return NotFound(new ApiResponse(404));
                string Base64 = ImageHellper.Base64String(ImageName);
                string SHA256 = ImageHellper.SHA256CheckSum(ImageName);

                logger.LogInformation("SUCCESSFULLY GET COUNTRY FLAG", isoCode);
                return new CountryFlagResponse
                {
                    fileName = isoCode + ".jpg",
                    fileBase64 = Base64,
                    sha256 = SHA256,
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