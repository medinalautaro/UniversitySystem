using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using SystemUniversity.Contracts.Repositories;
using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Persistence.Repositories
{
    internal class SubjectRepository : RepositoryDB<Subject>, ISubjectRepository
    {
        public SubjectRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task<IEnumerable<Subject>> SelectAllAsync()
        {
            string query = "SELECT * FROM university.subjects"; 

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] {});

            var subjectsList = new List<Subject>();

            while (reader.Read())
            {
                Subject subject = MapRowToModel(reader);

                subjectsList.Add(subject);
            }

            return subjectsList;    
        }

        public override async Task CreateAsync(Subject entity)
        {
            string query = "INSERT INTO university.subjects (name) VALUES ($1) RETURNING id;";
            int ID = await ExecuteScalarIntAsync(query, new object[] {entity.Name});
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int subjectId)
        {
            string query = "DELETE FROM university.subjects WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { subjectId });
        }

        public override async Task UpdateAsync(Subject entity)
        {
            string query = "UPDATE university.subjects SET name = $1 WHERE id = $2";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.Id});
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            string query = "SELECT name,id FROM university.subjects WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task RegisterProfessorAsync(int professorId, int subjectId)
        {
            string query = "INSERT INTO university.subjects_professors (subject_id, professor_id) VALUES ($1, $2);";
            await ExecuteNonQueryAsync(query, new object[] {subjectId, professorId});
        }

        public async Task RegisterStudentAsync(int studentId, int subjectId)
        {
            string query = "INSERT INTO university.subjects_students (subject_id, student_id) VALUES ($1, $2);";
            await ExecuteNonQueryAsync(query, new object[] {subjectId, studentId});
        }

        protected Subject MapRowToModel(NpgsqlDataReader reader)
        {
            return new Subject(
                    (string)reader["name"],
                    (int)reader["id"]
                    );
        }
    }
}
