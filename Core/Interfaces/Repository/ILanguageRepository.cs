using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface ILanguageRepository : IGenericRepository<Language>
    {
        Task<Language> FindByCode(string Code);
    }
}
