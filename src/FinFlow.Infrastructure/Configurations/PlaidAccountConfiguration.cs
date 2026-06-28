using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
    public class PlaidAccountConfiguration : BaseEntityConfiguration<PlaidAccount>
    {
        public override void Configure(EntityTypeBuilder<PlaidAccount> builder)
        {
            base.Configure(builder);

            builder.Ignore(pa => pa.IsActive);

            builder.HasKey(e => e.Id).HasName("plaidaccounts_pkey");

            builder.ToTable("plaidaccounts");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.EncryptedPlaidAccessToken)
                .HasComment("AES encrypted Plaid access token")
                .HasColumnName("encrypted_plaid_access_token");
            builder.Property(e => e.InstitutionId)
                .HasMaxLength(255)
                .HasColumnName("institution_id");
            builder.Property(e => e.InstitutionName)
                .HasMaxLength(255)
                .HasColumnName("institution_name");
            builder.Property(e => e.IsConnected)
                .HasDefaultValue(true)
                .HasColumnName("is_connected");
            builder.Property(e => e.LastSync)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_sync");
            builder.Property(e => e.PlaidAccountId)
                .HasMaxLength(255)
                .HasColumnName("plaid_account_id");
            builder.Property(e => e.PlaidItemId)
                .HasMaxLength(255)
                .HasColumnName("plaid_item_id");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.BankAccount).WithMany(p => p.PlaidAccounts)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plaidaccounts_bank_account_id_fkey");
        }
    }
}
