using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class BudgetCategoryConfiguration : BaseEntityConfiguration<BudgetCategory>
    {
        public override void Configure(EntityTypeBuilder<BudgetCategory> builder)
        {
            base.Configure(builder);

            builder.Ignore(bc => bc.IsActive);

            builder.HasKey(e => e.Id).HasName("budgetcategories_pkey");

            builder.ToTable("budgetcategories");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AllocatedAmount)
                .HasPrecision(15, 2)
                .HasColumnName("allocated_amount");
            builder.Property(e => e.BudgetId).HasColumnName("budget_id");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.IsOverBudget)
                .HasComputedColumnSql("(spent_amount > allocated_amount)", true)
                .HasColumnName("is_over_budget");
            builder.Property(e => e.RemainingAmount)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("(allocated_amount - spent_amount)", true)
                .HasColumnName("remaining_amount");
            builder.Property(e => e.SpentAmount)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("spent_amount");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.Budget).WithMany(p => p.BudgetCategories)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("budgetcategories_budget_id_fkey");

            builder.HasOne(d => d.Category).WithMany(p => p.BudgetCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("budgetcategories_category_id_fkey");
        }
    }
}
