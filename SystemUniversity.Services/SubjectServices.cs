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
    public class SubjectService : ISubjectService
    {
        public async Task<Subject> CreateAsync(string name)
        {
            ValidateSubject(name);
            Subject subject = new Subject(name);
            await Database.GetInstance().Subjects.CreateAsync(subject);

            return subject;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await Database.GetInstance().Subjects.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Id does not exist");
            }
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            return await Database.GetInstance().Subjects.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("The Id does not correspond to any course");
        }

        public async Task<IEnumerable<Subject>> SelectAllAsync()
        {
            return await Database.GetInstance().Subjects.SelectAllAsync();
        }

        public async Task<Subject> UpdateAsync(int id, string name)
        {
            ValidateSubject(name);

            Subject subject = await GetByIdAsync(id);

            subject.Name = name;

            await Database.GetInstance().Subjects.UpdateAsync(subject);

            return subject;
        }

        private void ValidateSubject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be empty");
            }  
        }
    }
}