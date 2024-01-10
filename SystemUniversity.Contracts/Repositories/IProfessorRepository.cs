using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Contracts.Repositories
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<Professor?> GetByNationalIdAsync(int national_id);
        Task<bool> ExistsByNationalIdAsync(string national_id);
        Task<List<Professor>> GetBySubjectAsync(Subject subject);
        Task<Professor?> GetByIdAsync(int id);
        
    }
}
