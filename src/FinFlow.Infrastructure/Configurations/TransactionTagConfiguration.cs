using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class TransactionTagConfiguration : BaseEntityConfiguration<TransactionTag>
    {
        public override void Configure(EntityTypeBuilder<TransactionTag> builder)
        {
            base.Configure(builder);

            builder.Ignore(tt => tt.IsActive);

            builder.HasKey(e => e.Id).HasName("transactiontags_pkey");

            builder.ToTable("transactiontags");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.TagName)
                .HasMaxLength(100)
                .HasColumnName("tag_name");
            builder.Property(e => e.TransactionId).HasColumnName("transaction_id");

            builder.HasOne(d => d.Transaction).WithMany(p => p.TransactionTags)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactiontags_transaction_id_fkey");
        }
    }
}
