using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;
using VasaGymnasiet.Models;

namespace VasaGymnasiet
{
    public class Personer
    {
        public static void AddPerson(Data.VasagymnasietContext context)
        {

            Console.WriteLine("Enter firstname:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter lastname:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter adress:");
            string adress = Console.ReadLine();
            Console.WriteLine("Enter socialsecuritynumber:");
            string socialSecurityNumber = Console.ReadLine();




            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(adress) ||
                string.IsNullOrWhiteSpace(socialSecurityNumber))
            {
                Console.WriteLine("All fields are required. Please try again.");
                return;
            }



            Console.WriteLine("Is the person an employee or student?");
            string Role = Console.ReadLine();

            if (Role != "employee" && Role != "student")
            {
                Console.WriteLine("invalid role, please try again!");
                return;
            }

            Person person = new Person
            {
                Name = firstName,
                LastName = lastName,
                Adress = adress,
                SocialSecurityNumber = socialSecurityNumber
            };



            try
            {
                context.Persons.Add(person);
                context.SaveChanges();
                Console.WriteLine("Person added successfully.");

                if (Role == "employee")
                {
                    Console.WriteLine("Please state a role, teacher,janitor or principal.");
                    string staffRole = Console.ReadLine();
                    if (staffRole != "teacher" && staffRole != "janitor" && staffRole != "principal")
                    {
                        Console.WriteLine("Invalid role, please try again.");
                        return;
                    }
                    Console.WriteLine("Please state a salary.");
                    decimal salary;
                    if (!decimal.TryParse(Console.ReadLine(), out salary))
                    {
                        Console.WriteLine("Invalid salary, please try again.");
                        return;
                    }
                    Employee employee = new Employee
                    {
                        FkPersonId = person.PersonId,
                        StaffRole = staffRole,
                        Salary = salary
                    };
                    context.Employees.Add(employee);
                }
                else if (Role == "student")
                {
                    Console.WriteLine("Please state a class for example SA24, TE23, NA23");
                    string className = Console.ReadLine();
                    Student student = new Student
                    {
                        FkPersonId = person.PersonId,
                        Class = className
                    };
                    context.Students.Add(student);
                }
                context.SaveChanges();
                Console.WriteLine("Person added successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving: {ex.Message}");
            }










        }
    }
}
