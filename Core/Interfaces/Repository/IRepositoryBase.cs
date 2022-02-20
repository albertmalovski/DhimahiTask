using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
   
        public interface IGenericRepository<T> where T : class
        {
            Task<ICollection<T>> FindAll();
            Task<T> FindById(int id);
            Task<bool> isExists(int id);
            Task<int> Create(T entity);
            Task<int> Update(T entity);
            Task<bool> Delete(T entity);
            Task<bool> Save();
        }
    }

