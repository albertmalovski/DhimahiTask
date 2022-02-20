using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ILanguageService
    {
        Task<int> CreateOrUpdate(string Code, string Name);
        Task<Language> GetByCode(string Code);
    }
}