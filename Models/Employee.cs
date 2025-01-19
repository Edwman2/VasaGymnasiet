using System;
using System.Collections.Generic;

namespace VasaGymnasiet.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int FkPersonId { get; set; }

    public string StaffRole { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateOnly EmploymentDate { get; set; }

    public string? Department { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Person FkPerson { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
