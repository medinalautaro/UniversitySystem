using SystemUniversity.Contracts.Repositories;
using SystemUniversity.Persistence.Repositories;
using Npgsql;

namespace SystemUniversity.Persistence
{
    public class Database: IDisposable
    {
        private readonly NpgsqlDataSource _dataSource;
        private static Database? instance = null; // this way and with a private constructor I make sure that there is only one instance of this database and initilizes when I call for it.

        private Database() //the mentioned private constructor
        {
            _dataSource = NpgsqlDataSource.Create("Host=127.0.0.1; Username=postgres; Password=frutilla;Database=postgres");
            Professors = new ProfessorRepository(_dataSource);
            Students = new StudentRepository(_dataSource);
            Subjects = new SubjectRepository(_dataSource);
        }

        public IProfessorRepository Professors { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ISubjectRepository Subjects { get; private set; }

        public void Dispose()
        {
            _dataSource.Dispose();
        }

        public static Database GetInstance() //creating instance
        {
            if (instance == null)
            {
                instance = new Database();
            }

            return instance;
        }
    }
}
