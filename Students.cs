using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;

namespace VasaGymnasiet
{
    public class Students
    {
        public static void GetStudents()
        {
            bool ExitMenu = false;

            while (!ExitMenu)
            {
                Console.WriteLine("=======================================================================================");
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("1. All students");
                Console.WriteLine("2. All classes");
                Console.WriteLine("3. Show all students with a specifik teacher");
                Console.WriteLine("4. Show all students in a specifik course");
                
                Console.WriteLine("=======================================================================================");


                string input = Console.ReadLine();

                using (var context = new VasagymnasietContext())
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("How do you want the names to be sorted? By Name or Lastname?");
                            string sortInput = Console.ReadLine();
                            // if statement to sort the names by name or lastname based on the user input
                            if (sortInput == "Name")
                            {
                                var students = context.Students.Select(s => new
                                {
                                    StudentId = s.StudentId,
                                    FirstName = s.FkPerson.Name,
                                    LastName = s.FkPerson.LastName,
                                    Class = s.Class
                                }).OrderBy(s => s.FirstName);

                                Console.WriteLine("Students");
                                Console.WriteLine("=======================================================================================");
                                foreach (var student in students)
                                {
                                    Console.WriteLine($"|Firstname: {student.FirstName}| |Lastname: {student.LastName}| Class: {student.Class}| |ID: {student.StudentId}|");
                                }
                                Console.WriteLine("=======================================================================================");
                                Console.WriteLine("Click enter to go back to menu");
                                Console.ReadLine();
                            }
                            else if (sortInput == "Lastname")
                            {
                                var students = context.Students.Select(s => new
                                {
                                    StudentId = s.StudentId,
                                    FirstName = s.FkPerson.Name,
                                    LastName = s.FkPerson.LastName,
                                    Class = s.Class
                                }).OrderBy(s => s.LastName);
                                
                                Console.WriteLine("Students");
                                Console.WriteLine("=======================================================================================");
                                foreach (var student in students)
                                {
                                    Console.WriteLine($"|Firstname: {student.FirstName}| |Lastname: {student.LastName}| Class: {student.Class}| |ID: {student.StudentId}|");
                                }
                                Console.WriteLine("=======================================================================================");
                                Console.WriteLine("Click enter to go back to menu");
                                Console.ReadLine();
                            }
                            // if the user input is incorrect the program will break
                            else
                                if (sortInput != "Name" || sortInput != "Lastname")
                            {
                                Console.WriteLine("Incorrect input");
                                break;
                            }
                            //else
                            //{
                            //    Console.WriteLine("Incorrect input");
                            //    break;
                            //    var students = context.Students.Select(s => new
                            //{
                            //    StudentId = s.StudentId,
                            //    FirstName = s.FkPerson.Name,
                            //    LastName = s.FkPerson.LastName,
                            //    Class = s.Class
                            //});

                            break;
                        case "2":
                            var Class = context.Students.Select(s => new
                            {
                                Class = s.Class
                            });

                            Console.WriteLine("Classes");
                            Console.WriteLine("=======================================================================================");
                            // Shows all the classes
                            foreach (var classes in Class)
                            {
                                Console.WriteLine($"Class: {classes.Class}");
                            }

                            Console.WriteLine("=======================================================================================");
                            // Gives the user the option to see the students in a specific class
                            Console.WriteLine("Choose which class you want to see");
                            Console.WriteLine("1. SA23 ");
                            Console.WriteLine("2. SA24 ");
                            Console.WriteLine("3. SA25 ");
                            Console.WriteLine("4. TE23 ");
                            Console.WriteLine("5. TE24 ");
                            Console.WriteLine("6. NA23 ");

                            string classInput = Console.ReadLine();


                            // Selects the students in the class the user chose
                            var studentsInClass = context.Students.Where(s => s.Class == "1")
                                .Select(s => new
                                {
                                    StudentId = s.StudentId,
                                    FirstName = s.FkPerson.Name,
                                    LastName = s.FkPerson.LastName,
                                    Class = s.Class
                                });
                            
                            foreach (var student in studentsInClass)
                            {
                                Console.WriteLine($"|Firstname: {student.FirstName}| |Lastname: {student.LastName}| |Class: {student.Class}| |ID: {student.StudentId}|");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();

                            break;

                        default:
                            Console.WriteLine("incorrect input");
                            break;


                    }
                }
            }
        }
    }
}
