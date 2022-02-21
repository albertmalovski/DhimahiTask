using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace API.ReferencedServices
{
    public class CountryInfoService
    {
        public static XNamespace xNamespace = "http://www.oorsprong.org/websamples.countryinfo";

        public static IEnumerable<XElement> GetCountryInfoService()
        {
            try
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

                        IEnumerable<XElement> responses = doc.Descendants(xNamespace + "tCountryInfo");
                        return responses;
                    }
                }
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
        }
    }
}
