namespace FinFlow.Domain.Entities
{
    public class BudgetCategory : BaseEntity
    {
        public decimal AllocatedAmount { get; set; }
        public decimal SpentAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsOverBudget { get; set; }
        public int BudgetId { get; set; }
        public int CategoryId { get; set; }
        public Budget Budget { get; set; }
        public Category Category { get; set; }
    }
}
