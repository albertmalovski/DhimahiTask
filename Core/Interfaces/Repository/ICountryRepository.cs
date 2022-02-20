using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> FindByCode(string Code);
        Task<List<Country>> FindByContientID(int ContinentID);
    }
}
