using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class FinancialGoalConfiguration : BaseEntityConfiguration<FinancialGoal>
    {
        public override void Configure(EntityTypeBuilder<FinancialGoal> builder)
        {
            base.Configure(builder);

            builder.Ignore(fg => fg.IsActive);

            builder.HasKey(e => e.Id).HasName("financialgoals_pkey");

            builder.ToTable("financialgoals", tb => tb.HasComment("User financial goals with progress tracking"));

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrentAmount)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("current_amount");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsCompleted)
                .HasDefaultValue(false)
                .HasColumnName("is_completed");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.TargetAmount)
                .HasPrecision(15, 2)
                .HasColumnName("target_amount");
            builder.Property(e => e.TargetDate).HasColumnName("target_date");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User).WithMany(p => p.FinancialGoals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("financialgoals_user_id_fkey");
        }
    }
}
