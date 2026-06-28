namespace FinFlow.Domain.Entities
{
    public class MonthlySpendingSummary
    {
        public string UserName { get; set; }
        public string Category { get; set; }
        public DateTime Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalSpent { get; set; }
        public long TransactionCount { get; set; }
        public int UserId { get; set; }
    }
}
