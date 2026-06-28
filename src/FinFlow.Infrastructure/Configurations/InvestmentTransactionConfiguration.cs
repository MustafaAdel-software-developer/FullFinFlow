using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class InvestmentTransactionConfiguration : BaseEntityConfiguration<InvestmentTransaction>
    {
        public override void Configure(EntityTypeBuilder<InvestmentTransaction> builder)
        {
            base.Configure(builder);

            builder.Ignore(it => it.IsActive);

            builder.HasKey(e => e.Id).HasName("investmenttransactions_pkey");

            builder.ToTable("investmenttransactions");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Fees)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("fees");
            builder.Property(e => e.InvestmentId).HasColumnName("investment_id");
            builder.Property(e => e.Notes).HasColumnName("notes");
            builder.Property(e => e.Price)
                .HasPrecision(15, 2)
                .HasColumnName("price");
            builder.Property(e => e.Quantity)
                .HasPrecision(15, 6)
                .HasColumnName("quantity");
            builder.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            builder.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.Investment).WithMany(p => p.InvestmentTransactions)
                .HasForeignKey(d => d.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("investmenttransactions_investment_id_fkey");
        }
    }
}
