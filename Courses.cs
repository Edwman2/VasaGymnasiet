using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;
using VasaGymnasiet.Models;

namespace VasaGymnasiet
{
    public class Courses
    {
        
        public static void GetCourses()
        {
            bool ExitMenu = false;
            while (!ExitMenu)
            {
                // Menu for the user to choose from which action to take
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("1. Show all courses");
                Console.WriteLine("2. Show all students in a specific course");
                Console.WriteLine("3. Show all courses and the average grade in every course");
                Console.WriteLine("4. Show ");
                Console.WriteLine("4. Exit");
                Console.WriteLine("=======================================================================================");

                string input = Console.ReadLine();

                using (var context = new VasagymnasietContext())
                {
                    switch (input)
                    {
                        // Show all courses
                        case "1":
                            var Courses = context.Courses.Select(e => new
                            {
                                CourseName = e.CourseName,
                                CourseID = e.CourseId
                            });
                            foreach (var c in Courses)
                            {
                                Console.WriteLine($"Coursename: {c.CourseName}, CourseID: {c.CourseID}");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();


                            break;
                        // Show all students in a specific course
                        case "2":

                            var studentsInCourse = context.Courses.Select(e => e.CourseName).Distinct().ToList();
                            Console.WriteLine("Coursename:");
                            foreach (var c in studentsInCourse)
                            {
                                Console.WriteLine($"- {c}");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Choose specifik course to see the students in it");
                            string courseInput = Console.ReadLine();
                            var InCourses = context.Courses
                                .Where(c => c.CourseName.ToLower() == courseInput.ToLower())
                                .Select(c => new
                                {
                                    CourseName = c.CourseName,
                                    Students = c.Students.Select(s => new
                                    {
                                        s.FkPerson.Name,
                                        s.FkPerson.LastName
                                    }).ToList()
                                })
                                .FirstOrDefault();


                            Console.WriteLine($"Students in the {courseInput} course");
                            Console.WriteLine("=======================================================================================");
                            foreach (var student in InCourses.Students)
                            {
                                Console.WriteLine($"|Name: {student.Name}| |Lastname: {student.LastName}|");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;
                        // Show all courses by name and the average grade in every course
                        case "3":
                            var averageCourse = context.Grades
                                .GroupBy(c => c.FkCourse.CourseName)
                                .Select(c => new
                                {
                                    CourseName = c.Key,
                                    AverageGrade = c.Average(g => Convert.ToDouble(g.Grade1))
                                })
                                .ToList();

                            foreach (var item in averageCourse)
                            {
                                Console.WriteLine($"Course: {item.CourseName} | Average Grade: {item.AverageGrade}");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;
                        // Exit
                        case "4":
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
