namespace FinFlow.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<BillReminder> BillReminders { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
