using SystemUniversity.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Models;
using SystemUniversity.Persistence.Repositories;
using SystemUniversity.Persistence;

namespace SystemUniversity.Services
{
    public class ProfessorService : IProfessorService
    {
        public async Task<Professor> CreateAsync(string professorName, string lastName, string nationalId)
        {
            await ValidateProfessorAsync(professorName, lastName, nationalId);
            Professor professor = new Professor(professorName, lastName, nationalId);
            await Database.Professors.CreateAsync(professor);

            return professor;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.Professors.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<Professor> GetByNationalIdAsync(int nationalId)
        {
            return await Database.Professors.GetByNationalIdAsync(nationalId)
                 ?? throw new KeyNotFoundException("The National Id does not correspond to any professor");
        }

        public async Task<IEnumerable<Professor>> SelectAllAsync()
        {
            return await Database.Professors.SelectAllAsync();
        }

        public async Task<Professor> UpdateAsync(int id, string name, string lastName, string nationalId)
        {
            Professor professor = await GetByNationalIdAsync(id);

            await ValidateProfessorAsync(name, lastName, nationalId, professor.NationalId != nationalId);

            professor.NationalId = nationalId;
            professor.LastName = lastName;
            professor.Name = name;

            await Database.Professors.UpdateAsync(professor);

            return professor;
        }

        private async Task ValidateProfessorAsync(string name, string lastName, string nationalId, bool checkIdRepeted = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException("Surname cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(nationalId))
            {
                throw new ArgumentNullException("National Id cannot be empty");
            }
            
            if (checkIdRepeted && await Database.Professors.ExistsByNationalIdAsync(nationalId))
            {
                throw new ArgumentException("National Id already exists");
            }
        }
    }
}