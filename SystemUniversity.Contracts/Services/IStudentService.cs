using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Contracts.Services
{
    public interface IStudentService
    {
        Task<Student> CreateAsync(string studentName, string lastName, string nationalId);
        Task<Student> UpdateAsync(int id, string name, string lastName, string nationalId);
        Task DeleteAsync(int id);
        Task<Student> GetByNationalIdAsync(int nationalId);
        Task<IEnumerable<Student>> SelectAllAsync();
    }
}
