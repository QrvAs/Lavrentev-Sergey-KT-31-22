using LavrentevKT3122lb1.Database.Helpers;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        private const string TableName = "cd_department";

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.Id)
                .HasName($"pk_{TableName}_id");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType(ColumnType.Long)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор кафедры");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_name")
                .HasColumnType($"{ColumnType.String}(200)")
                .HasMaxLength(200)
                .HasComment("Название кафедры");

            builder.Property(p => p.FoundationDate)
                .HasColumnName("d_foundation")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата основания кафедры");

            builder.Property(p => p.HeadId)
                .HasColumnName("f_head_id")
                .HasColumnType(ColumnType.Long)
                .HasComment("Идентификатор заведующего кафедрой");

            builder.HasOne(d => d.Head)
                .WithOne()
                .HasForeignKey<Department>(d => d.HeadId)
                .HasConstraintName($"fk_{TableName}_f_head_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.HeadId)
                .HasDatabaseName($"idx_{TableName}_f_head_id");

            builder.Navigation(d => d.Head)
                .AutoInclude();
        }
    }

}
