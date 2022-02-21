using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;
using System.Collections.Generic;
using Infrastructure.Exceptions;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

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
            try
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
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }

        public async Task<int> CreateOrUpdateLang(int CountryID, List<int> langID)
        {
            try
            {
                ICollection<CountryLanguage> ICL = new Collection<CountryLanguage>();
                Country coun = await countryRepository.FindById(CountryID);
                foreach (int id in langID)
                {
                    Language language = await languageRepository.FindById(id);
                    CountryLanguage countryLanguage = new CountryLanguage();
                    countryLanguage.Country = coun;
                    countryLanguage.Language = language;
                    countryLanguage.CountryId = coun.Id;
                    countryLanguage.LanguageId = language.Id;
                    countryLanguage.Id = id + coun.Id;
                    bool ExistLang = true; 
                    foreach(CountryLanguage CL in coun.CountryLanguage)
                    {
                        if (CL.LanguageId == id)
                        {
                            ExistLang = false;
                            break;
                        }
                    }
                    if(ExistLang)
                        coun.CountryLanguage.Add(countryLanguage);
                }
                return await countryRepository.Update(coun);
            }
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }

        public Task<Country> GetByCode(string Code)
        {
            try
            {
                return countryRepository.FindByCode(Code);
            }
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }

        public Task<List<Country>> GetByContinentID(int ContienentID)
        {
            try
            {
                return countryRepository.FindByContientID(ContienentID);
            }
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }
    }
}