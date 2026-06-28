using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class RecurringTransactionConfiguration : BaseEntityConfiguration<RecurringTransaction>
    {
        public override void Configure(EntityTypeBuilder<RecurringTransaction> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("recurringtransactions_pkey");

            builder.ToTable("recurringtransactions");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            builder.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.LastProcessedDate).HasColumnName("last_processed_date");
            builder.Property(e => e.NextDueDate).HasColumnName("next_due_date");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.BankAccount).WithMany(p => p.RecurringTransactions)
                .HasForeignKey(d => d.BankAccountId)
                .HasConstraintName("recurringtransactions_bank_account_id_fkey");

            builder.HasOne(d => d.Category).WithMany(p => p.RecurringTransactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("recurringtransactions_category_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.RecurringTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recurringtransactions_user_id_fkey");
        }
    }
}
