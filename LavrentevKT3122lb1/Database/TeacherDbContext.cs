using LavrentevKT3122lb1.Models;
using LavrentevKT3122lb1.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database
{
    public class TeacherDbContext : DbContext
    {
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherWorkload> Workloads { get; set; }

        public TeacherDbContext(DbContextOptions<TeacherDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AcademicDegreeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherWorkloadConfiguration());
        }
    }
}