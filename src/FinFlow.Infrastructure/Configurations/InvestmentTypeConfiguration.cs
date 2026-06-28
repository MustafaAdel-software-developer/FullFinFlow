using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class InvestmentTypeConfiguration : BaseEntityConfiguration<InvestmentType>
    {
        public override void Configure(EntityTypeBuilder<InvestmentType> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("investmenttypes_pkey");

            builder.ToTable("investmenttypes");

            builder.HasIndex(e => e.Name, "investmenttypes_name_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        }
    }
}
