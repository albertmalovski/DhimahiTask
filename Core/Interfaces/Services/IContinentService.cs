using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IContinentService
    {
        Task<int> CreateOrUpdate(string Code, string Name);
        Task<Continent> GetByCode(string Code);
    }
}