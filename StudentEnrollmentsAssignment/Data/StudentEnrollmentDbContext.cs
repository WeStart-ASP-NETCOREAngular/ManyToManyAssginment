using Microsoft.EntityFrameworkCore;
using StudentEnrollmentsAssignment.Models;

namespace StudentEnrollmentsAssignment.Data
{
    public class StudentEnrollmentDbContext : DbContext
    {
        public StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "John", LastName = "Stone" },
                new Student { Id = 2, FirstName = "Mia", LastName = "Wong" }
                );

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Title = "Developing with ASP.net core", Description = "60 hours" },
                new Class { Id = 2, Title = "Angular SPA", Description = "50 hours" }
                );

            modelBuilder.Entity<Enrollment>().HasKey(x => new { x.StudentId, x.ClassId });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

    }
}
