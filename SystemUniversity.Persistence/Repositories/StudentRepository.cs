using SystemUniversity.Contracts.Models;
using SystemUniversity.Contracts.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemUniversity.Persistence.Repositories
{
    internal class StudentRepository : RepositoryDB<Student>, IStudentRepository
    {
        public StudentRepository(NpgsqlDataSource dataSource) : base(dataSource) { }

        public override async Task<IEnumerable<Student>> SelectAllAsync(){
            string query = "SELECT * FROM university.students"; 

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] {});

            var studentsList = new List<Student>();

            while (reader.Read())
            {
                Student student = MapRowToModel(reader);

                studentsList.Add(student);
            }

            return studentsList;    
        }

        public async Task<Student?> GetByNationalIdAsync(int national_id)
        {
            string query = "SELECT (name,last_name,national_id,id) FROM university.students WHERE national_id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] {national_id});

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT FROM university.students WHERE national_id = $1", new[] { national_id });
        }

        public async Task<List<Student>> GetBySubjectAsync(Subject subject)
        {
            string query = "SELECT students.* FROM students " +
                "JOIN subjects_students ON students.id = subjects_students.student_id " +
                "JOIN subjects ON subjects.id = subjects_students.subject_id " +
                "WHERE subjects.id = $1;";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { subject.Id });

            var listastudents = new List<Student>();

            while (reader.Read())
            {
                var student = MapRowToModel(reader);

                listastudents.Add(student);
            }

            return listastudents;
        }

        public override async Task CreateAsync(Student entity)
        {
            string query = "INSERT INTO university.students (name, last_name, national_id) VALUES ($1, $2, $3) RETURNING id;";
            int ID = await ExecuteScalarIntAsync(query, new object[] {entity.Name, entity.LastName, entity.NationalId});
            entity.Id = ID;
        }

        public override async Task DeleteAsync(int studentId)
        {
            string query = "DELETE FROM university.students WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { studentId });
        }

        public override async Task UpdateAsync(Student entity)
        {
            string query = "UPDATE university.students SET name = $1, last_name = $2, national_id = $3 WHERE id = $4";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.LastName, entity.NationalId, entity.Id});
        }

        protected Student MapRowToModel(NpgsqlDataReader reader)
        {
            return new Student(
                    (string)reader["name"],
                    (string)reader["last_name"],
                    (string)reader["national_id"],
                    (int)reader["id"]);
        }
    }
}
