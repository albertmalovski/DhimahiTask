using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface ICurrencyRepository : IGenericRepository<Currency>
    {
        Task<Currency> FindByCode(string Code);
    }
}
