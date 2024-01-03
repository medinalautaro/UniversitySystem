using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Contracts.Services
{
    public interface IProfessorService
    {
        Task<Professor> CreateAsync(string professorName, string lastName, string nationalId);
        Task<Professor> UpdateAsync(int id, string name, string lastName, string nationalId);
        Task DeleteAsync(int id);
        Task<Professor> GetByNationalIdAsync(int nationalId);
        Task<IEnumerable<Professor>> SelectAllAsync();

        
    }
}
