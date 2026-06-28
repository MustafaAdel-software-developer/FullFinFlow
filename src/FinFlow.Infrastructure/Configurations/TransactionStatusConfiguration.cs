using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class TransactionStatusConfiguration : BaseEntityConfiguration<TransactionStatus>
    {
        public override void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("transactionstatuses_pkey");

            builder.ToTable("transactionstatuses");

            builder.HasIndex(e => e.Name, "transactionstatuses_name_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#000000'::character varying")
                .HasColumnName("color");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        }
    }
}
