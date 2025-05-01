using LavrentevKT3122lb1.Database.Helpers;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database.Configurations
{
    public class AcademicDegreeConfiguration : IEntityTypeConfiguration<AcademicDegree>
    {
        private const string TableName = "cd_academic_degree";

        public void Configure(EntityTypeBuilder<AcademicDegree> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.Id)
                .HasName($"pk_{TableName}_id");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType(ColumnType.Long)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор ученой степени");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_name")
                .HasColumnType($"{ColumnType.String}(100)")
                .HasMaxLength(100)
                .HasComment("Название ученой степени");

            builder.Property(p => p.IsDeleted)
                .HasColumnName("f_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Флаг удаления");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("d_created")
                .HasColumnType(ColumnType.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Дата создания");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("d_updated")
                .HasColumnType(ColumnType.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasComment("Дата обновления");
        }
    }
}
