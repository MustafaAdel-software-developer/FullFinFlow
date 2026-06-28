namespace FinFlow.Domain.Entities
{
    public class Budget : BaseEntity
    {
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public int BudgetPeriodId { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public BudgetPeriod BudgetPeriod { get; set; }
        public ICollection<BudgetCategory> BudgetCategories { get; set; }
    }
}
