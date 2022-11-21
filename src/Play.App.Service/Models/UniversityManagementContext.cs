using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Play.App.Service.Models
{
    public partial class UniversityManagementContext : DbContext
    {
        public UniversityManagementContext()
        {
        }

        public UniversityManagementContext(DbContextOptions<UniversityManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessCode> AccessCodes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Cfu).HasColumnName("CFU");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.TeacherNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Teacher)
                    .HasConstraintName("FK_Courses_Teacher");
            });

            

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Exam");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Grade).HasColumnName("Grade");

                entity.Property(e => e.Honors)
                    .HasMaxLength(50)
                    .HasColumnName("honors");

                entity.HasOne(d => d.Course)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Exam_Courses");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Exam_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.MatriculationId);

                entity.ToTable("Student");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Surname)
                    .HasMaxLength(100)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Surname).HasMaxLength(100);
            });

             modelBuilder.Entity<AccessCode>(entity =>
            {
                entity.Property(e => e.AccessCode).HasColumnName("AccessCode");

                entity.Property(e => e.Teacher).HasColumnName("AccessCode");

                entity.HasOne(d => d.TeacherNavigation)
                    .WithMany(p => p.AccessCodes)
                    .HasForeignKey(d => d.Teacher)
                    .HasConstraintName("FK_AccessCodes_AccessCodes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
