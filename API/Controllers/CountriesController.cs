using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using API.Errors;
using System.Security.Cryptography;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;

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
        //[Authorize]
        [HttpGet("populate")]
        public async Task<ActionResult<bool>> PopulateDataBase()
        {
            XNamespace ns = "http://www.oorsprong.org/websamples.countryinfo";
            IEnumerable<XElement> result = CountryInfoService();
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
                int CountryID = await (countryService.CreateOrUpdate(sISOCode, sName, sCapitalCity, sPhoneCode, sCountryFlag,ContinentID, CurrencyID));

                IEnumerable<XElement> languages = element.Descendants(ns + "tLanguage");
                List<int> langIDs = new List<int>();
                foreach (XElement lang in languages)
                {
                    string langISOCode = (string)lang.Element(ns + "sISOCode");
                    string langName = (string)lang.Element(ns + "sName");
                    int languageID = await (languageService.CreateOrUpdate(langISOCode, langName));
                    langIDs.Add(languageID);
                }

                //int Inserted = await (countryService.CreateOrUpdateLang(CountryID, langIDs));
            }

            return true;
        }
        public static IEnumerable<XElement> CountryInfoService()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
            webRequest.ContentType = "application/soap+xml;charset=UTF-8";
            webRequest.Method = "POST";
            HttpWebRequest request = webRequest;
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                        <soap:Body>
                                            <FullCountryInfoAllCountries xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                                            </FullCountryInfoAllCountries>
                                        </soap:Body>
                                      </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XDocument doc = XDocument.Parse(soapResult);
                    XNamespace ns = "http://www.oorsprong.org/websamples.countryinfo";
                    IEnumerable<XElement> responses = doc.Descendants(ns + "tCountryInfo");
                    return responses;
                }
            }
        }

        //The purpose of this endpoint is to return file contents to the client
        //[Authorize]
        [HttpPost("{isoCode}/flag")]
        public async Task<ActionResult<CountryFlagResponse>> GetCountryFlag(string isoCode)
        {
            string ImageName = ImagesLocation + isoCode + ".jpg";
            Country country = await (countryService.GetByCode(isoCode));
            string URL = country.CountryFlag;

            bool SuccessDownloaded = true;
            if (!checkIsDownloaded(ImageName))
            {
                SuccessDownloaded = DownloadImage(URL, ImageName);
            }
            if(!SuccessDownloaded) return NotFound(new ApiResponse(404));
            
            string Base64 = Base64String(ImageName);
            string SHA256 = SHA256CheckSum(ImageName);

            return new CountryFlagResponse
            {
                fileName = isoCode + ".jpg",
                fileBase64 = Base64,
                sha256 = SHA256,
            };
        }
        private string SHA256CheckSum(string filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
        private string Base64String(string filePath)
        {
            Byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }
        private bool checkIsDownloaded(string ImageName)
        {
            return System.IO.File.Exists(ImageName);
        }
        private bool DownloadImage(string URL, string ImageName)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(URL);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            yourImage.Save(ImageName, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            return true;
        }
    }

}