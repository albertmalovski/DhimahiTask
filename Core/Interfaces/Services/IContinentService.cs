using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IContinentService
    {
        Task<Continent> GetByCode(string Code);
    }
}