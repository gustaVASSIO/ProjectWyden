using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

namespace Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Dicipline> Diciplines { get; set; }

        /*
        public DbSet<CourseDicipline> CourseDicipline { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseDicipline>()
                .ToTable("coursedicipline")
                .HasKey(cd => new { cd.CoursesCourseId, cd.DiciplinesDiciplineId });
        }
        */

    }

}
