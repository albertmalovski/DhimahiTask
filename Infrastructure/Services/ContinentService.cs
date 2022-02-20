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
    }
}