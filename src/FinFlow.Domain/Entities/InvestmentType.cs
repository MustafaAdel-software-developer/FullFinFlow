namespace FinFlow.Domain.Entities
{
    public class InvestmentType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public ICollection<Investment> Investments { get; set; }
    }
}
