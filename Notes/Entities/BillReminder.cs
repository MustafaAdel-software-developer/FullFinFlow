namespace FinFlow.Domain.Entities
{
    public class BillReminder : BaseEntity
    {
        public string? BillName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Frequency { get; set; }
        public bool IsPaid { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int CurrencyId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public Currency Currency { get; set; }
    }
}
