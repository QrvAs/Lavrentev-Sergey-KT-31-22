using LavrentevKT3122lb1.Database.Helpers;
using LavrentevKT3122lb1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LavrentevKT3122lb1.Database.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        private const string TableName = "cd_position";

        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.Id)
                .HasName($"pk_{TableName}_id");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType(ColumnType.Long)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор должности");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_name")
                .HasColumnType($"{ColumnType.String}(100)")
                .HasMaxLength(100)
                .HasComment("Название должности");

            
            builder.Property(p => p.IsDeleted)
                  .HasColumnName("f_deleted")
                  .HasColumnType(ColumnType.Bool)
                  .HasDefaultValue(false)
                  .HasComment("Флаг удаления записи");

            
            builder.Property(p => p.CreatedAt)
                  .HasColumnName("d_created")
                  .HasColumnType(ColumnType.Date)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .HasComment("Дата создания записи");

          
            builder.Property(p => p.UpdatedAt)
                  .HasColumnName("d_updated")
                  .HasColumnType(ColumnType.Date)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasComment("Дата обновления записи");

           
            builder.HasIndex(p => p.Name)
                  .HasDatabaseName($"idx_{TableName}_name")
                  .IsUnique();

        }
    }

}
