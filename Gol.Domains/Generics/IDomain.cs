using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gol.Domains.Generics
{
    public interface IDomain<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
