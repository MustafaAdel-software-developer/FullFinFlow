using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class BudgetConfiguration : BaseEntityConfiguration<Budget>
    {
        public override void Configure(EntityTypeBuilder<Budget> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("budgets_pkey");

            builder.ToTable("budgets", tb => tb.HasComment("User-defined budgets with time periods"));

            builder.HasIndex(e => e.IsActive, "idx_budgets_active").HasFilter("(is_active = true)");

            builder.HasIndex(e => e.UserId, "idx_budgets_user_id");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BudgetPeriodId).HasColumnName("budget_period_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrencyId).HasColumnName("currency_id");
            builder.Property(e => e.EndDate).HasColumnName("end_date");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.StartDate).HasColumnName("start_date");
            builder.Property(e => e.TotalAmount)
                .HasPrecision(15, 2)
                .HasColumnName("total_amount");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.BudgetPeriod).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.BudgetPeriodId)
                .HasConstraintName("budgets_budget_period_id_fkey");

            builder.HasOne(d => d.Currency).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("budgets_currency_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("budgets_user_id_fkey");
        }
    }
}
