
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext dbContext;
        
        public LanguageRepository(AppDbContext context)
        {
            dbContext = context;  
        }
        public async Task<bool> Create(Language entity)
        {
            await dbContext.Languages.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Language entity)
        {
            dbContext.Languages.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Language>> FindAll()
        {
            var com = await dbContext.Languages.ToListAsync();
            return com;
        }

        public async Task<Language> FindById(int id)
        {
            var com = await dbContext.Languages.FindAsync(id);
            return com;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Language entity)
        {
            dbContext.Languages.Update(entity);
            return await Save();
        }


        public async Task<bool> isExists(int id)
        {
            var exist = await dbContext.Languages.AnyAsync(q => q.Id == id);
            return exist;
        }

        public async Task<Language> FindByCode(string Code)
        {
            var l = await dbContext.Languages.FirstOrDefaultAsync(q => q.ISOCode == Code);
            return l;
        }
    }  
}
