using System;
using System.Collections.Generic;

namespace VasaGymnasiet.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int FkStudentId { get; set; }

    public int FkCourseId { get; set; }

    public int FkEmployeeId { get; set; }

    public int Grade1 { get; set; }

    public DateOnly DateAssigned { get; set; }

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Employee FkEmployee { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
