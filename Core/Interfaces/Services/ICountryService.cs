using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICountryService
    {
        Task<Country> GetByCode(string Code);
    }
}