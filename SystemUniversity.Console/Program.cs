using System.ComponentModel;
using System.IO.Compression;
using System.Text.RegularExpressions;
using SystemUniversity;
using SystemUniversity.Contracts.Models;
using SystemUniversity.Persistence;


class Programa
{

    public static void Main(string[] args)
    {

        Programa programa = new Programa();

        int optionselected;

        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("Elija una opcion:");
            Console.WriteLine("1) Agregar Materia");
            Console.WriteLine("2) Agregar Profesor e inscribirlo a Materia");
            Console.WriteLine("3) Agregar Alumno");
            Console.WriteLine("4) Anotar alumno a materia");
            Console.WriteLine("5) Desanotar alumno de una materia");
            Console.WriteLine("6) Ver profesores de determinado alumno");
            Console.WriteLine("7) Ver alumnos de determinado profesor");
            Console.WriteLine("8) Salir");
            optionselected = int.Parse(Console.ReadLine());

            switch (optionselected)
            {
                case 1:
                    CrearMateriasPrueba();
                    break;
                case 2:
                    CrearProfesoresPrueba();
                    break;
                case 3:
                    CrearAlumnosPrueba();
                    break;
                case 4:
                    Database.GetInstance().Subjects.RegisterProfessorAsync(27, 4);
                    Database.GetInstance().Subjects.RegisterProfessorAsync(28, 4);
                    Database.GetInstance().Subjects.RegisterProfessorAsync(29, 4);
                    break;
                case 5:
                    Database.GetInstance().Subjects.RegisterStudentAsync(35, 4);
                    Database.GetInstance().Subjects.RegisterStudentAsync(31, 4);
                    Database.GetInstance().Subjects.RegisterStudentAsync(32, 4);
                    Database.GetInstance().Subjects.RegisterStudentAsync(37, 4);
                    break;
                case 6:
                    Database.GetInstance().Students.SelectAllAsync();
                    break;
                case 7:
                    break;
                case 8:
                    break;

                case 9:
                    break;

                default:
                    Console.WriteLine("Elegi bien capo");
                    break;
            }

            Console.WriteLine();
        }

        void CrearMateriasPrueba(){
            Database.GetInstance().Subjects.CreateAsync(new Subject("Matematica"));
            Database.GetInstance().Subjects.CreateAsync(new Subject("Fisica"));
            Database.GetInstance().Subjects.CreateAsync(new Subject("Historia"));
        }
        
        void CrearProfesoresPrueba(){
            Database.GetInstance().Professors.CreateAsync(new Professor("Jorge", "Batidor", "123456789"));
            Database.GetInstance().Professors.CreateAsync(new Professor("Raul", "Salamandra", "987654321"));
            Database.GetInstance().Professors.CreateAsync(new Professor("Jose", "Meconio", "234567890"));
            Database.GetInstance().Professors.CreateAsync(new Professor("Jerardo", "Rivas", "876543210"));
            Database.GetInstance().Professors.CreateAsync(new Professor("Miguel", "Almirante", "345678901"));
        }

        void CrearAlumnosPrueba(){
            Database.GetInstance().Students.CreateAsync(new Student("Carolina", "Gimenez", "789012345"));
            Database.GetInstance().Students.CreateAsync(new Student("Milagros", "Lassy", "456789012"));
            Database.GetInstance().Students.CreateAsync(new Student("Misty", "Mountains", "890123456"));
            Database.GetInstance().Students.CreateAsync(new Student("Mirta", "Legrand", "567890123"));
            Database.GetInstance().Students.CreateAsync(new Student("Manuel", "Portillo", "901234567"));
            Database.GetInstance().Students.CreateAsync(new Student("Fran", "Clarison", "654321098"));
            Database.GetInstance().Students.CreateAsync(new Student("Fede", "Porton", "109876543"));
            Database.GetInstance().Students.CreateAsync(new Student("Lautaro", "Smith", "321098765"));
            Database.GetInstance().Students.CreateAsync(new Student("Pedro", "Barras", "765432109"));
            Database.GetInstance().Students.CreateAsync(new Student("Mateo", "Boquita", "210987654"));
            Database.GetInstance().Students.CreateAsync(new Student("Jonathan", "Miles", "543210987"));
            Database.GetInstance().Students.CreateAsync(new Student("Sashimi", "Paso", "876543210"));
            Database.GetInstance().Students.CreateAsync(new Student("Monociclo", "Gutierras", "234567890"));
        }

        
    }
}

