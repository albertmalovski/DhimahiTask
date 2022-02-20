using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IContinentRepository : IGenericRepository<Continent>
    {
        Task<Continent> FindByCode(string Code);
    }
}
