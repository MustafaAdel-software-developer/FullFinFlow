using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("users_pkey");

            builder.ToTable("users", tb => tb.HasComment("Main user accounts for the personal finance application"));

            builder.HasIndex(e => e.IsActive, "idx_users_active").HasFilter("(is_active = true)");

            builder.HasIndex(e => e.Email, "idx_users_email");

            builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            builder.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            builder.Property(e => e.Password)
                .HasMaxLength(255)
                .HasComment("BCrypt hashed password for security")
                .HasColumnName("password");
            builder.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            builder.Property(e => e.RefreshTokenExpiry)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refresh_token_expiry");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        }
    }
}
