using SystemUniversity.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemUniversity.Contracts.Repositories
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task RegisterProfessorAsync(int professorId, int subjectId);
        Task RegisterStudentAsync(int studentId, int subjectId);
        Task<Subject?> GetByIdAsync(int id);

    }
}
