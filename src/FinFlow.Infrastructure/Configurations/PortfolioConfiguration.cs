using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        public override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("portfolios_pkey");

            builder.ToTable("portfolios", tb => tb.HasComment("Collections of investments for organization"));

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.TotalReturn)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_return");
            builder.Property(e => e.TotalReturnPercent)
                .HasPrecision(8, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_return_percent");
            builder.Property(e => e.TotalValue)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_value");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("portfolios_user_id_fkey");
        }
    }
}
