using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class BillReminderConfiguration : BaseEntityConfiguration<BillReminder>
    {
        public override void Configure(EntityTypeBuilder<BillReminder> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("billreminders_pkey");

            builder.ToTable("billreminders");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            builder.Property(e => e.BillName)
                .HasMaxLength(255)
                .HasColumnName("bill_name");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrencyId).HasColumnName("currency_id");
            builder.Property(e => e.DueDate).HasColumnName("due_date");
            builder.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.IsPaid)
                .HasDefaultValue(false)
                .HasColumnName("is_paid");
            builder.Property(e => e.Notes).HasColumnName("notes");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Category).WithMany(p => p.BillReminders)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("billreminders_category_id_fkey");

            builder.HasOne(d => d.Currency).WithMany(p => p.BillReminders)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("billreminders_currency_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.BillReminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("billreminders_user_id_fkey");
        }
    }
}
