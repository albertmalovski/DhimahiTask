
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext dbContext;
        
        public CountryRepository(AppDbContext context)
        {
            dbContext = context;  
        }
        public async Task<int> Create(Country entity)
        {
            await dbContext.Countries.AddAsync(entity);
            await Save();
            return entity.Id;
        }

        public async Task<bool> Delete(Country entity)
        {
            dbContext.Countries.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Country>> FindAll()
        {
            var country = await dbContext.Countries.ToListAsync();
            return country;
        }

        public async Task<Country> FindById(int id)
        {
            var country = await dbContext.Countries.Where(c => c.Id == id).Include(c => c.CountryLanguage).FirstOrDefaultAsync();
            return country;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<int> Update(Country entity)
        {
            dbContext.Countries.Update(entity);
            await Save();
            return entity.Id;
        }
        public async Task<Country> FindByCode(string Code)
        {
            var l = await dbContext.Countries.FirstOrDefaultAsync(q => q.ISOCode == Code);
            return l;
        }


        public async Task<bool> isExists(int id)
        {
            var exist = await dbContext.Countries.AnyAsync(q => q.Id == id);
            return exist;
        }

        public async Task<List<Country>> FindByContientID(int ContinentID)
        {
            return await (dbContext.Countries.Where(c => c.ContinentId == ContinentID)
                .Include(c => c.CountryLanguage).ThenInclude(l => l.Language)
                .ToListAsync());
        }
    }
}
