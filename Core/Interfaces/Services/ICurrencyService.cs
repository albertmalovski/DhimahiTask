using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICurrencyService
    {
        Task<Currency> GetByCode(string Code);
    }
}