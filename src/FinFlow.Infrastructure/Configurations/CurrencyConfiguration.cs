using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class CurrencyConfiguration : BaseEntityConfiguration<Currency>
    {
        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            base.Configure(builder);

            builder.Ignore(c => c.IsActive);

            builder.HasKey(e => e.Id).HasName("currencies_pkey");

            builder.ToTable("currencies");

            builder.HasIndex(e => e.Code, "currencies_code_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Code)
                .HasMaxLength(3)
                .HasColumnName("code");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.ExchangeRate)
                .HasPrecision(10, 6)
                .HasDefaultValueSql("1.0")
                .HasColumnName("exchange_rate");
            builder.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            builder.Property(e => e.Symbol)
                .HasMaxLength(10)
                .HasColumnName("symbol");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        }
    }
}
