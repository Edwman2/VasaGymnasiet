using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;

namespace VasaGymnasiet
{
    public class Classes
    {
        public static void GetClasses()
        {
            bool ExitMenu = false;

            while (!ExitMenu)
            {
                Console.WriteLine("=======================================================================================");
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("1. Show all classes ");
                Console.WriteLine("2. Show students from a specifik class");
                Console.WriteLine("=======================================================================================");

                string input = Console.ReadLine();

                using (var context = new VasagymnasietContext())
                {
                    switch (input)
                    {
                        case "1":
                            var classes = context.Students.Select(e => e.Class).Distinct().ToList();

                            Console.WriteLine("Classes");
                            foreach (var c in classes)
                            { Console.WriteLine($"Class: {c}"); }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;
                        case "2":
                            var studentsInClass = context.Students.Select(e => e.Class)
                                .Distinct().ToList();

                            Console.WriteLine("Classname:");
                            foreach (var c in studentsInClass)
                            {
                                Console.WriteLine($"- {c}");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Choose on class to see the students in that specifik class");

                            string classInput = Console.ReadLine();


                            var students = context.Students
                            .Where(e => e.Class == classInput)
                            .Select(e => new
                            {
                                studentID = e.StudentId,
                                FkPerson = e.FkPersonId,
                                Class = e.Class
                            })
                            .ToList();

                            var personIds = students.Select(s => s.FkPerson).ToList();
                            var persons = context.Persons.Where(p => personIds.Contains(p.PersonId)).ToList();

                            foreach (var s in students)
                            {
                                var person = persons.FirstOrDefault(person => person.PersonId == s.FkPerson);
                                if (person != null)
                                {
                                    Console.WriteLine($"|StudentID: {s.studentID}| |Name: {person.Name} {person.LastName}| |Class: {s.Class}|");

                                }
                                else
                                {
                                    Console.WriteLine("There is no students in this class");


                                }
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;

                        default:
                            Console.WriteLine("incorrect input");
                            break;
                    }
                    Console.WriteLine("=======================================================================================");
                    Console.WriteLine("Choose from the menu");
                    Console.WriteLine("1. Employees");
                    Console.WriteLine("2. Students");
                    Console.WriteLine("3. Classes");
                    Console.WriteLine("4. Courses");
                    Console.WriteLine("5. Add person to the system");
                    Console.WriteLine("=======================================================================================");

                    string input1 = Console.ReadLine();
                    Console.WriteLine("=======================================================================================");
                    switch (input1)
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
}
