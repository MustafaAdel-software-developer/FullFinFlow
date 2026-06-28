using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class ReportConfiguration : BaseEntityConfiguration<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);

            builder.Ignore(rp => rp.IsActive);

            builder.HasKey(e => e.Id).HasName("reports_pkey");

            builder.ToTable("reports");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrencyId).HasColumnName("currency_id");
            builder.Property(e => e.EndDate).HasColumnName("end_date");
            builder.Property(e => e.FilePath)
                .HasMaxLength(500)
                .HasColumnName("file_path");
            builder.Property(e => e.GeneratedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("generated_at");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            builder.Property(e => e.ReportName)
                .HasMaxLength(255)
                .HasColumnName("report_name");
            builder.Property(e => e.ReportType)
                .HasMaxLength(100)
                .HasColumnName("report_type");
            builder.Property(e => e.StartDate).HasColumnName("start_date");
            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Currency).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("reports_currency_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reports_user_id_fkey");
        }
    }
}
