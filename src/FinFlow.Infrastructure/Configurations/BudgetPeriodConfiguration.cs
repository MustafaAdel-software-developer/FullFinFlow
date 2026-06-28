using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class BudgetPeriodConfiguration : BaseEntityConfiguration<BudgetPeriod>
    {
        public override void Configure(EntityTypeBuilder<BudgetPeriod> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("budgetperiods_pkey");

            builder.ToTable("budgetperiods");

            builder.HasIndex(e => e.Name, "budgetperiods_name_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DaysInPeriod).HasColumnName("days_in_period");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        }
    }
}
