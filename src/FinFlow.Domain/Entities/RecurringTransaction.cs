namespace FinFlow.Domain.Entities
{
    public class RecurringTransaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public string? Frequency { get; set; }
        public string? Description { get; set; } 
        public DateTime NextDueDate { get; set; }
        public DateTime LastProcessedDate { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int BankAccountId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
