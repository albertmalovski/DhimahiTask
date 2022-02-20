using Core.Entities;
using Core.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<StoreRoles> StoreRoles { get; }
        IGenericRepository<Currency> Currency { get; }
        IGenericRepository<Language> Language { get; }
        IGenericRepository<Country> Country { get; }
        IGenericRepository<Continent> Continent { get; }
        Task<int> Complete();

    }
}