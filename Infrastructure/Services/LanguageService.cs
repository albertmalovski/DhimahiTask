using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;

namespace Infrastructure.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public async Task<Language> CreateOrUpdate(string Code, string Name)
        {
            Task<Language> language = languageRepository.FindByCode(Code);
            if (language != null && language.Result != null)
            {
                language.Result.UpdateAt = System.DateTime.Now;
                language.Result.Name = Name;
            }
            else
            {

                language.Result.CreatedAt = System.DateTime.Now;
                language.Result.ISOCode = Code;
                language.Result.Name = Name;
            }
            return await language;
        }

        public Task<Language> GetByCode(string Code)
        {
            return languageRepository.FindByCode(Code);
        }
    }
}