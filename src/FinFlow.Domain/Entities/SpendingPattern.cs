namespace FinFlow.Domain.Entities
{
    public class SpendingPattern : BaseEntity
    {
        public decimal AverageAmount { get; set; }
        public string? Frequency { get; set; }
        public DateTime PredictedNextDate { get; set; }
        public decimal Confidence { get; set; }
        public DateTime AnaylsisDate { get; set; }
        public string PatternType { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
    }
}
