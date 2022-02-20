using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICountryService
    {
        Task<int> CreateOrUpdate(string ISOCode, string Name, string CapitalCity, 
                                 string PhoneCode, string CountryFlag, int ContinentID, int CurrencyID);
        Task<Country> GetByCode(string Code);
        Task<int> CreateOrUpdateLang(int CountryID, List<int> langID);
        Task<List<Country>> GetByContinentID(int ContienentID);
    }
}