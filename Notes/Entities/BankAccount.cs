namespace FinFlow.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string EncryptedAccountNumber { get; set; }
        public string EncryptedRoutingNumber { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public int? PlaidAccountId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public PlaidAccount PlaidAccount { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
    }
}
