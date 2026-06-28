using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFlow.Infrastructure.Configurations
{
   public class BankAccountConfiguration : BaseEntityConfiguration<BankAccount>
   {
        public override void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id).HasName("bankaccounts_pkey");

            builder.ToTable("bankaccounts");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AccountName)
                .HasMaxLength(255)
                .HasColumnName("account_name");
            builder.Property(e => e.AccountType)
                .HasMaxLength(50)
                .HasColumnName("account_type");
            builder.Property(e => e.Balance)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("balance");
            builder.Property(e => e.BankName)
                .HasMaxLength(255)
                .HasColumnName("bank_name");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            builder.Property(e => e.CurrencyId).HasColumnName("currency_id");
            builder.Property(e => e.EncryptedAccountNumber)
                .HasComment("AES encrypted account number")
                .HasColumnName("encrypted_account_number");
            builder.Property(e => e.EncryptedRoutingNumber).HasColumnName("encrypted_routing_number");
            builder.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            builder.Property(e => e.PlaidAccountId)
                .HasMaxLength(255)
                .HasColumnName("plaid_account_id");
            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Currency).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("bankaccounts_currency_id_fkey");

            builder.HasOne(d => d.User).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bankaccounts_user_id_fkey");
        }
            
   }
}
