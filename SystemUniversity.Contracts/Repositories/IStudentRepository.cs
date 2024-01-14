using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Contracts.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByNationalIdAsync(int national_id);
        Task<bool> ExistsByNationalIdAsync(string national_id);
        Task<List<Student>> GetBySubjectAsync(Subject subject);
    
    }
}
