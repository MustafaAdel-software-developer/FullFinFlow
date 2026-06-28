namespace FinFlow.Domain.Entities
{
    public class Portfolio : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalReturn { get; set; }
        public decimal TotalReturnPercent { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Investment> Investments { get; set; }
    }
}
