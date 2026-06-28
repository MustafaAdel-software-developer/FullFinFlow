using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.Ignore(t => t.IsActive);

            builder.HasKey(e => e.Id).HasName("transactions_pkey");

            builder.ToTable("transactions", tb => tb.HasComment("Financial transactions linked to bank accounts"));

            builder.HasIndex(e => e.Amount, "idx_transactions_amount");

            builder.HasIndex(e => e.CategoryId, "idx_transactions_category");

            builder.HasIndex(e => e.TransactionDate, "idx_transactions_date");

            builder.HasIndex(e => e.BankAccountId, "idx_transactions_user_id");

            builder.HasIndex(e => e.TransactionId, "transactions_transaction_id_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasComment("Positive for income, negative for expenses")
                .HasColumnName("amount");
            builder.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.IsExcludedFromBudget)
                .HasDefaultValue(false)
                .HasColumnName("is_excluded_from_budget");
            builder.Property(e => e.IsRecurring)
                .HasDefaultValue(false)
                .HasColumnName("is_recurring");
            builder.Property(e => e.MerchantName)
                .HasMaxLength(255)
                .HasColumnName("merchant_name");
            builder.Property(e => e.Notes).HasColumnName("notes");
            builder.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            builder.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .HasColumnName("transaction_id");
            builder.Property(e => e.TransactionStatusId).HasColumnName("transaction_status_id");
            builder.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.BankAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactions_bank_account_id_fkey");

            builder.HasOne(d => d.Category).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("transactions_category_id_fkey");

            builder.HasOne(d => d.TransactionStatus).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionStatusId)
                .HasConstraintName("transactions_transaction_status_id_fkey");
        }
    }
}
