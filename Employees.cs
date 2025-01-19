using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;

namespace VasaGymnasiet
{
    public class Employees
    {
        public static void GetEmployees()
        {
               
            bool ExitMenu = false;


            while (!ExitMenu)
            {
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("1. Show all employees");
                Console.WriteLine("2. Principal");
                Console.WriteLine("3. Teachers");
                Console.WriteLine("4. Janitors");
                Console.WriteLine("5 Quantity per department");
                Console.WriteLine("6. Exit");
                


                Console.WriteLine("=======================================================================================");

                string input = Console.ReadLine();


                using (var context = new VasagymnasietContext())
                {
                    switch (input)

                    {
                        case "1":

                            // Gets all employees from the database and selects the information we want to show
                            var employees = context.Employees.Select(e => new
                            {
                                EmployeeId = e.EmployeeId,
                                FirstName = e.FkPerson.Name,
                                LastName = e.FkPerson.LastName,
                                Role = e.StaffRole,
                                Salary = e.Salary
                            });

                            Console.WriteLine("Employees");
                            Console.WriteLine("=======================================================================================");
                            // Prints out the information we selected from the database for each employee
                            foreach (var employee in employees)
                            {
                                Console.WriteLine($"{employee.FirstName} {employee.LastName}| Role: |{employee.Role}| Salary: |{employee.Salary}kr| ID: |{employee.EmployeeId}|");
                            }

                            Console.WriteLine("Click enter to go back to the menu");
                            Console.ReadLine();


                            break;
                        case "2":
                            var principal = context.Employees.Where(e => e.StaffRole == "Principal")
                                .Select(e => new
                                {
                                    EmployeeId = e.EmployeeId,
                                    FirstName = e.FkPerson.Name,
                                    LastName = e.FkPerson.LastName,
                                    Role = e.StaffRole,
                                    Salary = e.Salary
                                });
                            Console.WriteLine("Principalr");
                            Console.WriteLine("=======================================================================================");
                            foreach (var prince in principal)
                            {
                                Console.WriteLine($"{prince.FirstName} {prince.LastName}| |Role: {prince.Role}| |Salary: {prince.Salary}kr| |ID: {prince.EmployeeId}|");
                            }
                            Console.WriteLine("Click enter to go back to the menu");
                            Console.ReadLine();


                            break;
                        case "3":



                            var teachers = context.Employees.Where(e => e.StaffRole == "Teacher")
                                .Select(e => new
                                {
                                    EmployeeId = e.EmployeeId,
                                    FirstName = e.FkPerson.Name,
                                    LastName = e.FkPerson.LastName,
                                    Role = e.StaffRole,
                                    Salary = e.Salary
                                });

                            Console.WriteLine("Teacher");
                            Console.WriteLine("=======================================================================================");
                            foreach (var teacher in teachers)
                            {
                                Console.WriteLine($"{teacher.FirstName} {teacher.LastName}| |Role: {teacher.Role}| |Salary: {teacher.Salary}kr| |ID: {teacher.EmployeeId}|");
                            }
                            Console.WriteLine("Click enter to go back to the menu");
                            Console.ReadLine();

                            break;
                        case "4":
                            var janitors = context.Employees.Where(e => e.StaffRole == "Janitor")
                                .Select(e => new
                                {
                                    EmployeeId = e.EmployeeId,
                                    FirstName = e.FkPerson.Name,
                                    LastName = e.FkPerson.LastName,
                                    Role = e.StaffRole,
                                    Salary = e.Salary
                                });
                            Console.WriteLine("Janitor");
                            Console.WriteLine("=======================================================================================");
                            foreach (var janitor in janitors)
                            {
                                Console.WriteLine($"{janitor.FirstName} {janitor.LastName}| |Role: {janitor.Role}| |Salary: {janitor.Salary}kr| |ID: {janitor.EmployeeId}|");
                            }
                            Console.WriteLine("Click enter to go back to the menu");
                            Console.ReadLine();

                            break;
                            case "5":
                                var quantity = context.Employees
                                .GroupBy(e => e.Department)
                                .Select(e => new
                                {
                                    Department = e.Key,
                                    Quantity = e.Count()
                                });

                            foreach (var q in quantity)
                            {
                                Console.WriteLine($"Department: {q.Department} | Quantity: {q.Quantity}");
                            }
                            Console.WriteLine("Click enter to go back to the menu");
                            Console.ReadLine();
                            break;

                        case "6":
                            ExitMenu = true;
                            Console.WriteLine("Exiting...");
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
