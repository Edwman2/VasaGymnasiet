using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VasaGymnasiet.Models;

namespace VasaGymnasiet.Data;

public partial class VasagymnasietContext : DbContext
{
    public VasagymnasietContext()
    {
    }

    public VasagymnasietContext(DbContextOptions<VasagymnasietContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = Vasagymnasiet; Integrated Security = True; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71870FBC5704");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEmployeeId).HasColumnName("FkEmployeeID");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__FkEmplo__29572725");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF11D023B24");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FkPersonId).HasColumnName("FkPersonID");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StaffRole)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkPerson).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__FkPer__267ABA7A");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A37AB4F751C");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.DateAssigned).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FkCourseId).HasColumnName("FkCourseID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FkEmployeeID");
            entity.Property(e => e.FkStudentId).HasColumnName("FkStudentID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__FkCourse__35BCFE0A");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__FkEmploy__36B12243");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grades__FkStuden__34C8D9D1");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Persons__AA2FFB858B1F0183");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SocialSecurityNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A798FB54050");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkCourseId).HasColumnName("FkCourseID");
            entity.Property(e => e.FkPersonId).HasColumnName("FkPersonID");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__FkCour__2D27B809");

            entity.HasOne(d => d.FkPerson).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__FkPers__2C3393D0");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => e.StudentCourseId).HasName("PK__StudentC__7E3E2FB25524FF05");

            entity.Property(e => e.StudentCourseId).HasColumnName("StudentCourseID");
            entity.Property(e => e.FkCourseId).HasColumnName("FkCourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("FkStudentID");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__FkCou__30F848ED");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__FkStu__300424B4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
