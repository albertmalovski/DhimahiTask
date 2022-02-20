
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
        public async Task<bool> Create(Country entity)
        {
            await dbContext.Countries.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Country entity)
        {
            dbContext.Countries.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Country>> FindAll()
        {
            var brands = await dbContext.Countries.ToListAsync();
            return brands;
        }

        public async Task<Country> FindById(int id)
        {
            var brands = await dbContext.Countries.FindAsync(id);
            return brands;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Country entity)
        {
            dbContext.Countries.Update(entity);
            return await Save();
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
    }
}
