using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly ILanguageRepository languageRepository;

        public CountryService(ICountryRepository countryRepository, ILanguageRepository languageRepository)
        {
            this.countryRepository = countryRepository;
            this.languageRepository = languageRepository;
        }

        public async Task<int> CreateOrUpdate(string ISOCode, string Name, string CapitalCity, 
                                        string PhoneCode, string CountryFlag, int ContinentID, int CurrencyID)
        {
            Task<Country> country = countryRepository.FindByCode(ISOCode);
            if (country != null && country.Result != null)
            {
                country.Result.UpdateAt = System.DateTime.Now;
                country.Result.Name = Name;
                country.Result.CapitalCity = CapitalCity;
                country.Result.PhoneCode = PhoneCode;
                country.Result.CountryFlag = CountryFlag;
                country.Result.ContinentId = ContinentID;
                country.Result.CurrencyId = CurrencyID;
                return await countryRepository.Update(country.Result);
            }
            else
            {
                Country NewCountry = new Country();
                NewCountry.CreatedAt = System.DateTime.Now;
                NewCountry.ISOCode = ISOCode;
                NewCountry.Name = Name;
                NewCountry.CapitalCity = CapitalCity;
                NewCountry.PhoneCode = PhoneCode;
                NewCountry.CountryFlag = CountryFlag;
                NewCountry.ContinentId = ContinentID;
                NewCountry.CurrencyId = CurrencyID;
                return await countryRepository.Create(NewCountry);
            }
        }

        public Task<int> CreateOrUpdateLang(int CountryID, List<int> langID)
        {
            ICollection<CountryLanguage> ICL = new Collection<CountryLanguage>();
            foreach(int id in langID)
            {
                Task<Language> language = languageRepository.FindById(id);
                Task<Country> coun = countryRepository.FindById(CountryID);
                CountryLanguage countryLanguage = new CountryLanguage();
                countryLanguage.Country = coun.Result;
                countryLanguage.Language = language.Result;
                countryLanguage.CountryId = coun.Result.Id;
                countryLanguage.LanguageId = language.Result.Id;
                countryLanguage.CreatedAt = System.DateTime.Now;
                countryLanguage.UpdateAt = System.DateTime.Now;
                countryLanguage.Id = id + coun.Result.Id;
                ICL.Add(countryLanguage);
            }

            Task<Country> country = countryRepository.FindById(CountryID);
            country.Result.CountryLanguage = ICL;
            return countryRepository.Create(country.Result);
        }

        public Task<Country> GetByCode(string Code)
        {
            return countryRepository.FindByCode(Code);
        }

        public Task<List<Country>> GetByContinentID(int ContienentID)
        {
            return countryRepository.FindByContientID(ContienentID);
        }
    }
}