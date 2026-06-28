namespace FinFlow.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public bool IsDefault { get; set; }
        public bool IsIncome { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<TaxCategory> TaxCategories  { get; set; }
        public ICollection<SpendingPattern> SpendingPatterns { get; set; }
        public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
        public ICollection<BillReminder> BillReminders { get; set; }
        public ICollection<BudgetCategory> BudgetCategories { get; set; }
    }
}
