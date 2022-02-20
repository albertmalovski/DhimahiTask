
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Specifications;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly AppDbContext dbContext;
        
        public ContinentRepository(AppDbContext context)
        {
            dbContext = context;  
        }
        public async Task<bool> Create(Continent entity)
        {
            await dbContext.Continents.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Continent entity)
        {
            dbContext.Continents.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Continent>> FindAll()
        {
            var com = await dbContext.Continents.ToListAsync();
            return com;
        }

        public async Task<Continent> FindById(int id)
        {
            var com = await dbContext.Continents.FindAsync(id);
            return com;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Continent entity)
        {
            dbContext.Continents.Update(entity);
            return await Save();
        }

        public async Task<Continent> GetCompanyByNUISAsync(int companyNuis)
        {
            var com = await dbContext.Continents.FindAsync(companyNuis);
            return com;
        }

        public async Task<bool> isExists(int id)
        {
            var exist = await dbContext.Continents.AnyAsync(q => q.Id == id);
            return exist;
        }
        public async Task<Continent> FindByCode(string Code)
        {
            var l = await dbContext.Continents.FirstOrDefaultAsync(q => q.Code == Code);
            return l;
        }
    }
}
