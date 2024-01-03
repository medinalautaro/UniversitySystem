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
                    Database.Subjects.RegisterProfessorAsync(27, 4);
                    Database.Subjects.RegisterProfessorAsync(28, 4);
                    Database.Subjects.RegisterProfessorAsync(29, 4);
                    break;
                case 5:
                    Database.Subjects.RegisterStudentAsync(35, 4);
                    Database.Subjects.RegisterStudentAsync(31, 4);
                    Database.Subjects.RegisterStudentAsync(32, 4);
                    Database.Subjects.RegisterStudentAsync(37, 4);
                    break;
                case 6:
                    Database.Students.SelectAllAsync();
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
            Database.Subjects.CreateAsync(new Subject("Matematica"));
            Database.Subjects.CreateAsync(new Subject("Fisica"));
            Database.Subjects.CreateAsync(new Subject("Historia"));
        }
        
        void CrearProfesoresPrueba(){
            Database.Professors.CreateAsync(new Professor("Jorge", "Batidor", "123456789"));
            Database.Professors.CreateAsync(new Professor("Raul", "Salamandra", "987654321"));
            Database.Professors.CreateAsync(new Professor("Jose", "Meconio", "234567890"));
            Database.Professors.CreateAsync(new Professor("Jerardo", "Rivas", "876543210"));
            Database.Professors.CreateAsync(new Professor("Miguel", "Almirante", "345678901"));
        }

        void CrearAlumnosPrueba(){
            Database.Students.CreateAsync(new Student("Carolina", "Gimenez", "789012345"));
            Database.Students.CreateAsync(new Student("Milagros", "Lassy", "456789012"));
            Database.Students.CreateAsync(new Student("Misty", "Mountains", "890123456"));
            Database.Students.CreateAsync(new Student("Mirta", "Legrand", "567890123"));
            Database.Students.CreateAsync(new Student("Manuel", "Portillo", "901234567"));
            Database.Students.CreateAsync(new Student("Fran", "Clarison", "654321098"));
            Database.Students.CreateAsync(new Student("Fede", "Porton", "109876543"));
            Database.Students.CreateAsync(new Student("Lautaro", "Smith", "321098765"));
            Database.Students.CreateAsync(new Student("Pedro", "Barras", "765432109"));
            Database.Students.CreateAsync(new Student("Mateo", "Boquita", "210987654"));
            Database.Students.CreateAsync(new Student("Jonathan", "Miles", "543210987"));
            Database.Students.CreateAsync(new Student("Sashimi", "Paso", "876543210"));
            Database.Students.CreateAsync(new Student("Monociclo", "Gutierras", "234567890"));
        }

        
    }
}

