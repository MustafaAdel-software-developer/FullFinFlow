namespace FinFlow.Domain.Entities
{
    public class TaxCategory : BaseEntity
    {
        public string? TaxCode { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsDeductible { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
