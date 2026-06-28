using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("categories_pkey");

            builder.ToTable("categories");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#3498db'::character varying")
                .HasColumnName("color");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.IsIncome)
                .HasDefaultValue(false)
                .HasColumnName("is_income");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            builder.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.ParentCategory).WithMany(p => p.ChildCategories)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("categories_parent_category_id_fkey");
        }
    }
}
