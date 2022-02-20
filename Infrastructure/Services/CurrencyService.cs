using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;

namespace Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        public Task<Currency> GetByCode(string Code)
        {
            return currencyRepository.FindByCode(Code);
        }
    }
}