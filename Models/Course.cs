using System;
using System.Collections.Generic;

namespace VasaGymnasiet.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int FkEmployeeId { get; set; }

    public virtual Employee FkEmployee { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
