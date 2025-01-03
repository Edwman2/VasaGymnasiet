using VasaGymnasiet.Models;
using VasaGymnasiet.Data;

namespace VasaGymnasiet
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
------------------------------------------------------------------------------------
|     __     __               ____                                 _      _        |    
|     \ \   / /_ _ ___  __ _ / ___|_   _ _ __ ___  _ __   __ _ ___(_) ___| |_      |
|      \ \ / / _` / __|/ _` | |  _| | | | '_ ` _ \| '_ \ / _` / __| |/ _ \ __|     |
|       \ V / (_| \__ \ (_| | |_| | |_| | | | | | | | | | (_| \__ \ |  __/ |_      |
|        \_/ \__,_|___/\__,_|\____|\__, |_| |_| |_|_| |_|\__,_|___/_|\___|\__|     |
|                                  |___/                                           |
|__________________________________________________________________________________|
");
            using (var context = new VasagymnasietContext())
            {
                // main menu to navigate through the system
                Console.WriteLine("=======================================================================================");
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Students");
                Console.WriteLine("3. Classes");
                Console.WriteLine("4. Courses");
                Console.WriteLine("5. Add person to the system");
                Console.WriteLine("=======================================================================================");

                string input = Console.ReadLine();
                Console.WriteLine("=======================================================================================");


                // switch case to use the different classes
                switch (input)
                {
                    case "1":
                        Employees.GetEmployees();
                        break;
                    case "2":
                        Students.GetStudents();
                        break;
                    case "3":
                        Classes.GetClasses();
                        break;
                    case "4":
                        Courses.GetCourses();
                        break;
                    case "5":
                        Personer.AddPerson(context);
                        break;




                    default:
                        Console.WriteLine("Felaktig inmatning");
                        break;
                }

            }
        }
    }
}
