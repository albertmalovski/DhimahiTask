using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepositoryGeneric<Currency> _currency;
        private IRepositoryGeneric<Country> _category;
        private IRepositoryGeneric<StoreRoles> _roles;
        private IRepositoryGeneric<Language> _languages;
        private IRepositoryGeneric<Continent> _continents;


        public IGenericRepository<StoreRoles> StoreRoles => throw new NotImplementedException();

        public IGenericRepository<Currency> Currency => throw new NotImplementedException();

        public IGenericRepository<Language> Language => throw new NotImplementedException();

        public IGenericRepository<Country> Country => throw new NotImplementedException();

        public IGenericRepository<Continent> Continent => throw new NotImplementedException();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                _context.Dispose();
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

    }
}