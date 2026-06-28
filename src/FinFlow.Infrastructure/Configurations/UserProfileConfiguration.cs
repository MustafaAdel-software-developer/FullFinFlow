using FinFlow.Domain.Entities;
using FinFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class UserProfileConfiguration : BaseEntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Ignore(up => up.IsActive);

            builder.HasKey(e => e.Id).HasName("userprofiles_pkey");

            builder.ToTable("userprofiles");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Address).HasColumnName("address");
            builder.Property(e => e.AnnualIncome)
                .HasPrecision(15, 2)
                .HasColumnName("annual_income");
            builder.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            builder.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Egypt'::character varying")
                .HasColumnName("country");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            builder.Property(e => e.ProfilePicture)
                .HasMaxLength(500)
                .HasColumnName("profile_picture");
            builder.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("zip_code");

            builder.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userprofiles_user_id_fkey");
        }
    }
}
