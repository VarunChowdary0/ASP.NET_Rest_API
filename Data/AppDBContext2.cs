using Microsoft.EntityFrameworkCore;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Data
{
    public class AppDBContext2 : DbContext
    {
        public AppDBContext2(DbContextOptions<AppDBContext2> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany()
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithOne(c => c.Grade)
                .HasForeignKey<Grade>(g => g.CourseId);
        }
    }
}