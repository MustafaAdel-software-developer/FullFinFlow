namespace FinFlow.Domain.Entities
{
    public class Investment : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal TotalReturn { get; set; }
        public decimal DayChange { get; set; }
        public decimal DayChangePercent { get; set; }
        public string AccountNumber { get; set; }
        public string Broker { get; set; }
        public int UserId { get; set; }
        public int PortfolioId { get; set; }
        public int InvestmentTypeId { get; set; }
        public User User { get; set; }
        public Portfolio Portfolio { get; set; }
        public InvestmentType InvestmentType { get; set; }
        public ICollection<InvestmentTransaction> InvestmentTransactions { get; set; }
    }
}
