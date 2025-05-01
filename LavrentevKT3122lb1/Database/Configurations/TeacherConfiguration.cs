using LavrentevKT3122lb1.Database.Helpers;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        private const string TableName = "cd_teacher";

        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.Id)
                .HasName($"pk_{TableName}_id");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType(ColumnType.Long)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор преподавателя");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_firstname")
                .HasColumnType($"{ColumnType.String}(100)")
                .HasMaxLength(100)
                .HasComment("Имя преподавателя");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("c_lastname")
                .HasColumnType($"{ColumnType.String}(100)")
                .HasMaxLength(100)
                .HasComment("Фамилия преподавателя");

            builder.Property(p => p.MiddleName)
                .HasColumnName("c_middlename")
                .HasColumnType($"{ColumnType.String}(100)")
                .HasMaxLength(100)
                .HasComment("Отчество преподавателя");

            builder.Property(p => p.BirthDate)
                .HasColumnName("d_birthdate")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата рождения");

            builder.Property(p => p.HireDate)
                .HasColumnName("d_hiredate")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата приема на работу");

            builder.Property(p => p.DepartmentId)
                .HasColumnName("f_department_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Ссылка на кафедру");

            builder.Property(p => p.PositionId)
                .HasColumnName("f_position_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Ссылка на должность");

            builder.Property(p => p.AcademicDegreeId)
                .HasColumnName("f_degree_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Ссылка на ученую степень");

            builder.HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .HasConstraintName($"fk_{TableName}_f_department_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Position)
                .WithMany(p => p.Teachers)
                .HasForeignKey(t => t.PositionId)
                .HasConstraintName($"fk_{TableName}_f_position_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.AcademicDegree)
                .WithMany(ad => ad.Teachers)
                .HasForeignKey(t => t.AcademicDegreeId)
                .HasConstraintName($"fk_{TableName}_f_degree_id")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(p => p.DepartmentId)
                .HasDatabaseName($"idx_{TableName}_f_department_id");

            builder.HasIndex(p => p.PositionId)
                .HasDatabaseName($"idx_{TableName}_f_position_id");

            builder.HasIndex(p => p.AcademicDegreeId)
                .HasDatabaseName($"idx_{TableName}_f_degree_id");


        }
    }
}
