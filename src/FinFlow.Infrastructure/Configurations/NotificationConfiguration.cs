using FinFlow.Domain.Entities;
using FinFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class NotificationConfiguration : BaseEntityConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.Ignore(n => n.IsActive);

            builder.HasKey(e => e.Id).HasName("notifications_pkey");

            builder.ToTable("notifications", tb => tb.HasComment("System notifications for users"));

            builder.HasIndex(e => e.IsRead, "idx_notifications_unread").HasFilter("(is_read = false)");

            builder.HasIndex(e => e.UserId, "idx_notifications_user_id");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ActionUrl)
                .HasMaxLength(500)
                .HasColumnName("action_url");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            builder.Property(e => e.Message).HasColumnName("message");
            builder.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            builder.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("notifications_notification_type_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_user_id_fkey");
        }
    }
}
