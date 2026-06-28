namespace FinFlow.Domain.Entities
{
    public class InvestmentTransaction : BaseEntity
    {
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Fees { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }
        public int InvestmentId { get; set; }
        public Investment Investment { get; set; }
    }
}
