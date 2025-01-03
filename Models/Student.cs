using System;
using System.Collections.Generic;

namespace VasaGymnasiet.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int FkPersonId { get; set; }

    public int FkCourseId { get; set; }

    public string Class { get; set; } = null!;

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Person FkPerson { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
