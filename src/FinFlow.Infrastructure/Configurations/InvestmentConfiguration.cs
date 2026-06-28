using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class InvestmentConfiguration : BaseEntityConfiguration<Investment>
    {
        public override void Configure(EntityTypeBuilder<Investment> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("investments_pkey");

            builder.ToTable("investments", tb => tb.HasComment("Investment holdings with current market values"));

            builder.HasIndex(e => e.PortfolioId, "idx_investments_portfolio");

            builder.HasIndex(e => e.Symbol, "idx_investments_symbol");

            builder.HasIndex(e => e.UserId, "idx_investments_user_id");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AccountNumber)
                .HasMaxLength(255)
                .HasColumnName("account_number");
            builder.Property(e => e.Broker)
                .HasMaxLength(255)
                .HasColumnName("broker");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrentPrice)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("current_price");
            builder.Property(e => e.CurrentValue)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("(quantity * current_price)", true)
                .HasComment("Calculated as quantity * current_price")
                .HasColumnName("current_value");
            builder.Property(e => e.DayChange)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("day_change");
            builder.Property(e => e.DayChangePercent)
                .HasPrecision(8, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("day_change_percent");
            builder.Property(e => e.InvestmentTypeId).HasColumnName("investment_type_id");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.PortfolioId).HasColumnName("portfolio_id");
            builder.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            builder.Property(e => e.PurchasePrice)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("purchase_price");
            builder.Property(e => e.Quantity)
                .HasPrecision(15, 6)
                .HasDefaultValueSql("0.00")
                .HasColumnName("quantity");
            builder.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
            builder.Property(e => e.TotalReturn)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("((current_price - purchase_price) * quantity)", true)
                .HasComment("Calculated as (current_price - purchase_price) * quantity")
                .HasColumnName("total_return");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.InvestmentType).WithMany(p => p.Investments)
                .HasForeignKey(d => d.InvestmentTypeId)
                .HasConstraintName("investments_investment_type_id_fkey");

            builder.HasOne(d => d.Portfolio).WithMany(p => p.Investments)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("investments_portfolio_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.Investments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("investments_user_id_fkey");

        }
    }
}
