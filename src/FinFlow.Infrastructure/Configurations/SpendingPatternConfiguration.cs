using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class SpendingPatternConfiguration : BaseEntityConfiguration<SpendingPattern>
    {
        public override void Configure(EntityTypeBuilder<SpendingPattern> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("spendingpatterns_pkey");

            builder.ToTable("spendingpatterns");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AnaylsisDate).HasColumnName("analysis_date");
            builder.Property(e => e.AverageAmount)
                .HasPrecision(15, 2)
                .HasColumnName("average_amount");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.Confidence)
                .HasPrecision(5, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("confidence");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.PatternType)
                .HasMaxLength(100)
                .HasColumnName("pattern_type");
            builder.Property(e => e.PredictedNextDate).HasColumnName("predicted_next_date");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Category).WithMany(p => p.SpendingPatterns)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("spendingpatterns_category_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.SpendingPatterns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("spendingpatterns_user_id_fkey");
        }
    }
}
