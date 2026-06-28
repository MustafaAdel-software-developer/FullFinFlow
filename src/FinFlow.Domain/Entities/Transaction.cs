namespace FinFlow.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? MerchantName { get; set; }
        public string? TransactionType { get; set; }
        public bool IsRecurring { get; set; }
        public string? Notes { get; set; }
        public bool IsExcludedFromBudget { get; set; }
        public int TransactionStatusId { get; set; }
        public int BankAccountId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public BankAccount BankAccount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public ICollection<TransactionTag> TransactionTags { get; set; }
    }
}
