using SystemUniversity.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Models;
using SystemUniversity.Persistence.Repositories;
using SystemUniversity.Persistence;
using SystemUniversity.Contracts.Exceptions;

namespace SystemUniversity.Services
{
    public class ProfessorService : IProfessorService
    {
        public async Task<Professor> CreateAsync(string professorName, string lastName, string nationalId)
        {
            await ValidateProfessorAsync(professorName, lastName, nationalId);
            Professor professor = new Professor(professorName, lastName, nationalId);
            await Database.GetInstance().Professors.CreateAsync(professor);

            return professor;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Professors.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Id does not exist");
            }
        }

        public async Task<Professor> GetByNationalIdAsync(int nationalId)
        {
            return await Database.GetInstance().Professors.GetByNationalIdAsync(nationalId)
                 ?? throw new NotFoundException("The National Id does not correspond to any professor");
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Professors.GetByIdAsync(id)
                 ?? throw new NotFoundException("The Id does not correspond to any professor");
        }

        public async Task<IEnumerable<Professor>> SelectAllAsync()
        {
            return await Database.GetInstance().Professors.SelectAllAsync();
        }

        public async Task<Professor> UpdateAsync(int id, string name, string lastName, string nationalId)
        {
            Professor professor = await GetByNationalIdAsync(id);

            await ValidateProfessorAsync(name, lastName, nationalId, professor.NationalId != nationalId);

            professor.NationalId = nationalId;
            professor.LastName = lastName;
            professor.Name = name;

            await Database.GetInstance().Professors.UpdateAsync(professor);

            return professor;
        }

        private async Task ValidateProfessorAsync(string name, string lastName, string nationalId, bool checkIdRepeted = true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException("Name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new BadRequestException("Surname cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(nationalId))
            {
                throw new BadRequestException("National Id cannot be empty");
            }

           
            if (checkIdRepeted && await Database.GetInstance().Professors.ExistsByNationalIdAsync(nationalId))
            {
                throw new BadRequestException("National Id already exists");
            }
            
            
        }
    }
}