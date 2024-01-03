using SystemUniversity.Contracts.Repositories;
using SystemUniversity.Persistence.Repositories;
using Npgsql;

namespace SystemUniversity.Persistence
{
    public class Database: IDisposable
    {
        private static readonly NpgsqlDataSource _dataSource;
        private static Database? instance = null; //TODO preguntar a nico porque pedro lo tiene de esta manera

        static Database()
        {
            _dataSource = NpgsqlDataSource.Create("Host=127.0.0.1; Username=postgres; Password=frutilla;Database=postgres");
            Professors = new ProfessorRepository(_dataSource);
            Students = new StudentRepository(_dataSource);
            Subjects = new SubjectRepository(_dataSource);
        }

        public static IProfessorRepository Professors { get; private set; }
        public static IStudentRepository Students { get; private set; }
        public static ISubjectRepository Subjects { get; private set; }

        public void Dispose()
        {
            _dataSource.Dispose();
        }

        public static Database GetInstance()
        {
            if (instance == null)
            {
                instance = new Database();
            }

            return instance;
        }
    }
}
