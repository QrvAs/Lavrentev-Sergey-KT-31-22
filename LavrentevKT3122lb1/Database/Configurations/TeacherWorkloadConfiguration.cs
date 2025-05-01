using LavrentevKT3122lb1.Database.Helpers;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database.Configurations
{
    public class TeacherWorkloadConfiguration : IEntityTypeConfiguration<TeacherWorkload>
    {
        private const string TableName = "cd_teacher_workload";

        public void Configure(EntityTypeBuilder<TeacherWorkload> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.Id)
                .HasName($"pk_{TableName}_id");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType(ColumnType.Long)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор нагрузки");

            builder.Property(p => p.Hours)
                .HasColumnName("n_hours")
                .HasColumnType(ColumnType.Int)
                .HasComment("Количество часов");

            builder.Property(p => p.Semester)
                .HasColumnName("n_semester")
                .HasColumnType(ColumnType.Int)
                .HasComment("Семестр (1 или 2)");

            builder.Property(p => p.Year)
                .HasColumnName("n_year")
                .HasColumnType(ColumnType.Int)
                .HasComment("Год");

            builder.Property(p => p.TeacherId)
                .HasColumnName("f_teacher_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Ссылка на преподавателя");

            builder.Property(p => p.DisciplineId)
                .HasColumnName("f_discipline_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Ссылка на дисциплину");

            builder.HasOne(tw => tw.Teacher)
                .WithMany(t => t.Workloads)
                .HasForeignKey(tw => tw.TeacherId)
                .HasConstraintName($"fk_{TableName}_f_teacher_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tw => tw.Discipline)
                .WithMany(d => d.Workloads)
                .HasForeignKey(tw => tw.DisciplineId)
                .HasConstraintName($"fk_{TableName}_f_discipline_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.TeacherId, p.DisciplineId, p.Semester, p.Year })
                .HasDatabaseName($"idx_{TableName}_teacher_discipline_unique")
                .IsUnique();

            builder.Navigation(tw => tw.Teacher)
                .AutoInclude();

            builder.Navigation(tw => tw.Discipline)
                .AutoInclude();
        }
    }
}
