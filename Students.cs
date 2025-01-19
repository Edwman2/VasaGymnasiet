using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VasaGymnasiet.Data;
using VasaGymnasiet.Models;

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
                Console.WriteLine("1. Show all students");
                Console.WriteLine("2. Show all courses");
                Console.WriteLine("3. Show all students in a specific course");
                Console.WriteLine("4. Show all courses and the average grade in every course");
                Console.WriteLine("5. Show grades from the last month");
                Console.WriteLine("6. Show active courses");
                Console.WriteLine("7. Exit");
                Console.WriteLine("=======================================================================================");


                string input = Console.ReadLine();

                using (var context = new Data.VasagymnasietContext())
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("How do you want the names to be sorted? By Name or Lastname?");
                            string sortInput = Console.ReadLine();
                            Console.WriteLine("Do you want the sorting to be Ascending or Descending?");
                            string sortInput2 = Console.ReadLine();
                            // if statement to sort the names by name or lastname based on the user input
                            if (sortInput == "Name" || sortInput == "Lastname")
                            {
                                if (sortInput2 == "Ascending" || sortInput2 == "Descending")
                                {
                                    var student = context.Students.Select(s => new
                                    {
                                        StudentId = s.StudentId,
                                        FirstName = s.FkPerson.Name,
                                        LastName = s.FkPerson.LastName,
                                        Class = s.Class
                                    });
                                    if (sortInput == "Name" && sortInput2 == "Ascending")
                                    {
                                        student = student.OrderBy(s => s.FirstName);
                                    }

                                    else if (sortInput == "Name" && sortInput2 == "Descending")
                                    {
                                        student = student.OrderByDescending(s => s.FirstName);
                                    }
                                    else if (sortInput == "Lastname" && sortInput2 == "Ascending")
                                    {
                                        student = student.OrderBy(s => s.LastName);
                                    }
                                    else if (sortInput == "Lastname" && sortInput2 == "Descending")
                                    {
                                        student = student.OrderByDescending(s => s.LastName);
                                    }

                                    Console.WriteLine("Students");
                                    Console.WriteLine("=======================================================================================");
                                    foreach (var students in student)
                                    {
                                        Console.WriteLine($"|Firstname: {students.FirstName}| |Lastname: {students.LastName}| Class: {students.Class}| |ID: {students.StudentId}|");
                                    }
                                    Console.WriteLine("=======================================================================================");
                                    Console.WriteLine("Click enter to go back to menu");
                                    Console.ReadLine();

                                }
                                else
                                {
                                    Console.WriteLine("Incorrect input for sorting, either write Ascending or Descending");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect input for sorting, either write Name or Lastname");
                            }


                            break;
                        case "2":
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
                        case "3":

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
                        case "4":
                            var averageGrade = context.Grades
                                .GroupBy(g => g.FkCourse.CourseName)
                                .Select(g => new
                                {
                                    CourseName = g.Key,
                                    AverageGrade = g.Average(g => g.Grade1),
                                    minGrade = g.Min(g => g.Grade1),
                                    maxGrade = g.Max(g => g.Grade1)
                                });
                            Console.WriteLine("Courses and the average grade in every course");
                            Console.WriteLine("=======================================================================================");
                            foreach (var grade in averageGrade)
                            {
                                Console.WriteLine($"|Course: {grade.CourseName}| |Average grade: {grade.AverageGrade:F2}| |Min grade: {grade.minGrade}| |Max grade: {grade.maxGrade}|");
                            }

                            break;
                        // Show grades from the last month
                        case "5":
                            var MonthBefore = DateTime.Now.AddMonths(- 1);
                            var grades = context.Grades
                                .Where(g => g.DateAssigned.Month == MonthBefore.Month && g.DateAssigned.Year ==  MonthBefore.Year)
                                .Select(g => new
                                {
                                    CourseName = g.FkCourse.CourseName,
                                    StudentName = g.FkStudent.FkPerson.Name,
                                    StudentLastName = g.FkStudent.FkPerson.LastName,
                                    Grade = g.Grade1,
                                    Teacher = g.FkEmployee.FkPerson.Name,
                                    Date = g.DateAssigned
                                });
                            Console.WriteLine("Grades from the last month");
                            Console.WriteLine("=======================================================================================");
                            foreach (var grade in grades)
                            {
                                Console.WriteLine($"|Course: {grade.CourseName}| |Student: {grade.StudentName} {grade.StudentLastName}| |Grade: {grade.Grade}| Teacher: {grade.Teacher}| |Date: {grade.Date}|");
                            }
                            Console.WriteLine("=======================================================================================");
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;

                        case "6":
                            var activeCourses = context.Grades
                            .Where(g => g.Status)
                            .Join(context.Students,
                            grade => grade.FkStudentId,
                           student => student.StudentId,
                           (grade, student) => new
                           {
                               grade.FkCourseId,
                               student.FkPersonId
                           })
                            .Join(context.Persons,
                            student => student.FkPersonId,
                            person => person.PersonId,
                            (student, person) => new
                            {
                                CourseID = student.FkCourseId,
                                StudenttName = person.Name + " " + person.LastName
                            })
                            .ToList();

                            if (activeCourses.Count == 0)
                            {
                                Console.WriteLine("No active courses");
                            }
                            else
                            {
                                foreach (var c in activeCourses)
                                {
                                    Console.WriteLine($"CourseID: {c.CourseID}, Student Name: {c.StudenttName}");
                                }
                            }
                            Console.WriteLine("Click enter to go back to menu");
                            Console.ReadLine();
                            break;


                        // Exit
                        case "7":
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
