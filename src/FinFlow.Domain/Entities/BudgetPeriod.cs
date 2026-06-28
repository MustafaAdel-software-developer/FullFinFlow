namespace FinFlow.Domain.Entities
{
    public class BudgetPeriod : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DaysInPeriod { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
