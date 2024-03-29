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
    public class StudentService : IStudentService
    {
        public async Task<Student> CreateAsync(string studentName, string lastName, string nationalId)
        {
            await ValidateStudentAsync(studentName, lastName, nationalId);
            Student student = new Student(studentName, lastName, nationalId);
            await Database.GetInstance().Students.CreateAsync(student);

            return student;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Students.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Id does not exist");
            }
        }

        public async Task<Student> GetByNationalIdAsync(int nationalId)
        {
            return await Database.GetInstance().Students.GetByNationalIdAsync(nationalId)
                 ?? throw new NotFoundException("The National Id does not correspond to any student");
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Students.GetByIdAsync(id)
                 ?? throw new NotFoundException("The Id does not correspond to any student");
        }

        public async Task<IEnumerable<Student>> SelectAllAsync()
        {
            return await Database.GetInstance().Students.SelectAllAsync();
        }

        public async Task<Student> UpdateAsync(int id, string name, string lastName, string nationalId)
        {
            Student student = await GetByIdAsync(id);

            await ValidateStudentAsync(name, lastName, nationalId, student.NationalId != nationalId);

            student.NationalId = nationalId;
            student.LastName = lastName;
            student.Name = name;

            await Database.GetInstance().Students.UpdateAsync(student);

            return student;
        }

        private async Task ValidateStudentAsync(string name, string lastName, string nationalId, bool checkIdRepeted = true)
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
            
            if (checkIdRepeted && await Database.GetInstance().Students.ExistsByNationalIdAsync(nationalId))
            {
                throw new BadRequestException("National Id already exists");
            }
        }
    }
}