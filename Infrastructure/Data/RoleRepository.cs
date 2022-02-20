
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext dbContext;
        
        public RoleRepository(AppDbContext context)
        {
            dbContext = context;  
        }
        public async Task<bool> Create(StoreRoles entity)
        {
            await dbContext.StoreRoles.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(StoreRoles entity)
        {
            dbContext.StoreRoles.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<StoreRoles>> FindAll()
        {
            var storeRoles = await dbContext.StoreRoles.ToListAsync();
            return storeRoles;
        }

        public async Task<StoreRoles> FindById(int id)
        {
            var storeRoles = await dbContext.StoreRoles.FindAsync(id);
            return storeRoles;
        }
        public async Task<bool> Save()
        {
            var changes = await dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(StoreRoles entity)
        {
            dbContext.StoreRoles.Update(entity);
            return await Save();
        }


        public async Task<bool> isExists(string id)
        {
            var exist = await dbContext.StoreRoles.AnyAsync(q => q.Id == id);
            return exist;
        }

        public Task<bool> isExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
