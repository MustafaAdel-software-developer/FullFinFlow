using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class StockPriceConfiguration : BaseEntityConfiguration<StockPrice>
    {
        public override void Configure(EntityTypeBuilder<StockPrice> builder)
        {
            base.Configure(builder);

            builder.Ignore(stp => stp.IsActive);

            builder.HasKey(e => e.Id).HasName("stockprices_pkey");

            builder.ToTable("stockprices");

            builder.HasIndex(e => e.PriceDate, "idx_stock_prices_date");

            builder.HasIndex(e => e.Symbol, "idx_stock_prices_symbol");

            builder.HasIndex(e => new { e.Symbol, e.PriceDate }, "stockprices_symbol_price_date_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ClosePrice)
                .HasPrecision(15, 2)
                .HasColumnName("close_price");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.HighPrice)
                .HasPrecision(15, 2)
                .HasColumnName("high_price");
            builder.Property(e => e.LowPrice)
                .HasPrecision(15, 2)
                .HasColumnName("low_price");
            builder.Property(e => e.OpenPrice)
                .HasPrecision(15, 2)
                .HasColumnName("open_price");
            builder.Property(e => e.PriceDate).HasColumnName("price_date");
            builder.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
            builder.Property(e => e.Volume).HasColumnName("volume");
        }
    }
}
