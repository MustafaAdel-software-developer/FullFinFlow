using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class TaxCategoryConfiguration : BaseEntityConfiguration<TaxCategory>
    {
        public override void Configure(EntityTypeBuilder<TaxCategory> builder)
        {
            base.Configure(builder);

            builder.Ignore(tt => tt.IsActive);

            builder.HasKey(e => e.Id).HasName("taxcategories_pkey");

            builder.ToTable("taxcategories");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsDeductible)
                .HasDefaultValue(false)
                .HasColumnName("is_deductible");
            builder.Property(e => e.TaxCode)
                .HasMaxLength(50)
                .HasColumnName("tax_code");
            builder.Property(e => e.TaxRate)
                .HasPrecision(5, 4)
                .HasDefaultValueSql("0.0")
                .HasColumnName("tax_rate");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.Category).WithMany(p => p.TaxCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taxcategories_category_id_fkey");
        }
    }
}
