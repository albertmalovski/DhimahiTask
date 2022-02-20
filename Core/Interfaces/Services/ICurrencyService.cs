using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICurrencyService
    {
        Task<int> CreateOrUpdate(string Code, string Name);
        Task<Currency> GetByCode(string Code);
    }
}