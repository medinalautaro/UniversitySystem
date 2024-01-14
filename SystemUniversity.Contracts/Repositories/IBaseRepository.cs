using SystemUniversity.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SystemUniversity.Contracts.Repositories
{
    public interface IRepository <T>
    {
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int entityId);
        public Task<IEnumerable<T>> SelectAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
    }
}
