using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemUniversity.Contracts.Repositories;
using SystemUniversity.Contracts.Models;

namespace SystemUniversity.Persistence.Repositories
{
    internal class ProfessorRepository : RepositoryDB<Professor>, IProfessorRepository
    {

        public ProfessorRepository(NpgsqlDataSource dataSource) : base(dataSource) { }


        public override async Task<IEnumerable<Professor>> SelectAllAsync(){
            string query = "SELECT * FROM university.professors"; 

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] {});

            var professorsList = new List<Professor>();

            while (reader.Read())
            {
                Professor Professor = MapRowToModel(reader);

                professorsList.Add(Professor);
            }

            return professorsList;    
        }

        public async Task<Professor?> GetByNationalIdAsync(int national_id)
        {
            string query = "SELECT name,last_name,national_id,id FROM university.professors WHERE national_id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] {national_id});

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<bool> ExistsByNationalIdAsync(string national_id)
        {
            return await ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT * FROM university.professors WHERE national_id = $1)", new[] { national_id });
        }

        public async Task<List<Professor>> GetBySubjectAsync(Subject subject)
        {
            string query = "SELECT professors.* FROM professors " +
                "JOIN subjects_professors ON professors.id = subjects_professors.professor_id " +
                "JOIN subjects ON subjects.id = subjects_professors.subject_id " +
                "WHERE subjects.id = $1;";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { subject.Id });

            var listaprofessors = new List<Professor>();

            while (reader.Read())
            {
                var professor = MapRowToModel(reader);

                listaprofessors.Add(professor);
            }

            return listaprofessors;
        }

        public override async Task<int> CreateAsync(Professor entity)
        {
            string query = "INSERT INTO university.professors (name, last_name, national_id) VALUES ($1, $2, $3) RETURNING id;";
            int ID = await ExecuteScalarIntAsync(query, new object[] {entity.Name, entity.LastName, entity.NationalId});
            entity.Id = ID;
            return ID;
        }

        public override async Task DeleteAsync(int professorId)
        {
            string query = "DELETE FROM university.professors WHERE id = $1";
            await ExecuteNonQueryAsync(query, new object[] { professorId });
        }

        public override async Task UpdateAsync(Professor entity)
        {
            string query = "UPDATE university.professors SET name = $1, last_name = $2, national_id = $3 WHERE id = $4";
            await ExecuteNonQueryAsync(query, new object[] { entity.Name, entity.LastName, entity.NationalId, entity.Id});
        }

        public async Task<Professor?> GetByIdAsync(int id)
        {
            string query = "SELECT name,last_name,national_id,id FROM university.professors WHERE id = $1";

            using NpgsqlDataReader reader = await GetQueryReaderAsync(query, new object[] { id });

            if (reader.Read())
            {
                return MapRowToModel(reader);
            }

            return null;
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await ExecuteScalarAsync<bool>("SELECT EXISTS(SELECT * FROM university.professors WHERE id = $1)", new[] { id });
        }

        protected Professor MapRowToModel(NpgsqlDataReader reader)
        {
            return new Professor(
                    (string)reader["name"],
                    (string)reader["last_name"],
                    (string)reader["national_id"],
                    (int)reader["id"]
                    );
        }

    }
}
