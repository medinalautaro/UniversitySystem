using SystemUniversity.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemUniversity.Contracts.Services
{
    public interface ISubjectService
    {
        Task<Subject> CreateAsync(string name);

        Task<Subject> UpdateAsync(int id, string name);

        Task DeleteAsync(int id);

        Task<Subject> GetByIdAsync(int id);

        Task<IEnumerable<Subject>> SelectAllAsync();
    }
}
