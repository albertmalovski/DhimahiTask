using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;

namespace Infrastructure.Services
{
    public class ContinentService : IContinentService
    {
        private readonly IContinentRepository continentRepository;

        public ContinentService(IContinentRepository continentRepository)
        {
            this.continentRepository = continentRepository;
        }

        public Task<Continent> GetByCode(string Code)
        {
            return continentRepository.FindByCode(Code);
        }
        public async Task<int> CreateOrUpdate(string Code, string Name)
        {
            Task<Continent> continent = continentRepository.FindByCode(Code);
            if (continent != null && continent.Result != null)
            {
                continent.Result.UpdateAt = System.DateTime.Now;
                continent.Result.Name = Name;
                return await continentRepository.Update(continent.Result);
            }
            else
            {
                Continent NewCont = new Continent();
                NewCont.CreatedAt = System.DateTime.Now;
                NewCont.Code = Code;
                NewCont.Name = Name;
                return await continentRepository.Create(NewCont);
            }
        }

    }
}