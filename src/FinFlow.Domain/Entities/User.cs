namespace FinFlow.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserProfile> UserProfiles { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
        public ICollection<Investment> Investments { get; set; }
        public ICollection<SpendingPattern> SpendingPatterns { get; set; }
        public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
        public ICollection<BillReminder> BillReminders { get; set; }
        public ICollection<FinancialGoal> FinancialGoals { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
