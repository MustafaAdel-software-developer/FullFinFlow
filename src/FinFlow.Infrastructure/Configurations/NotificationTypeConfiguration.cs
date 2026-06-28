using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class NotificationTypeConfiguration : BaseEntityConfiguration<NotificationType>
    {
        public override void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("notificationtypes_pkey");

            builder.ToTable("notificationtypes");

            builder.HasIndex(e => e.Name, "notificationtypes_name_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        }
    }
}
