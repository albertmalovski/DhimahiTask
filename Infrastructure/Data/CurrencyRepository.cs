
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext dbContext;
        
        public CurrencyRepository(AppDbContext context)
        {
            dbContext = context;  
        }
        public async Task<bool> Create(Currency entity)
        {
            await dbContext.Currencies.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Currency entity)
        {
            dbContext.Currencies.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Currency>> FindAll()
        {
            var com = await dbContext.Currencies.ToListAsync();
            return com;
        }

        public async Task<Currency> FindById(int id)
        {
            var com = await dbContext.Currencies.FindAsync(id);
            return com;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Currency entity)
        {
            dbContext.Currencies.Update(entity);
            return await Save();
        }


        public async Task<bool> isExists(int id)
        {
            var exist = await dbContext.Currencies.AnyAsync(q => q.Id == id);
            return exist;
        }

        public async Task<Currency> FindByCode(string Code)
        {
            var l = await dbContext.Currencies.FirstOrDefaultAsync(q => q.ISOCode == Code);
            return l;
        }
    }  
}
