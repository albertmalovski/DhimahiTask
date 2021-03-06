using System.Threading.Tasks;
using Core.Interfaces.Repository;
using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return currencyRepository.FindByCode(Code);
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
        public async Task<int> CreateOrUpdate(string Code, string Name)
        {
            try {
                Task<Currency> currency = currencyRepository.FindByCode(Code);
                if (currency != null && currency.Result != null)
                {
                    currency.Result.UpdateAt = System.DateTime.Now;
                    currency.Result.Name = Name;
                    return await currencyRepository.Update(currency.Result);
                }
                else
                {
                    Currency NewCurrency = new Currency();
                    NewCurrency.CreatedAt = System.DateTime.Now;
                    NewCurrency.ISOCode = Code;
                    NewCurrency.Name = Name;
                    return await currencyRepository.Create(NewCurrency);
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
    }
}