using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public async Task<int> CreateOrUpdate(string Code, string Name)
        {
            try
            {
                Task<Language> language = languageRepository.FindByCode(Code);
                if (language != null && language.Result != null)
                {
                    language.Result.UpdateAt = System.DateTime.Now;
                    language.Result.Name = Name;
                    return await languageRepository.Update(language.Result);
                }
                else
                {
                    Language NewLang = new Language();
                    NewLang.CreatedAt = System.DateTime.Now;
                    NewLang.ISOCode = Code;
                    NewLang.Name = Name;
                    return await languageRepository.Create(NewLang);
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }
        public Task<Language> GetByCode(string Code)
        {
            try
            {
                return languageRepository.FindByCode(Code);
            }
            catch (DbUpdateException ex)
            {
                throw new DBException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new GeneralException(ex.Message);
            }
        }
    }
}