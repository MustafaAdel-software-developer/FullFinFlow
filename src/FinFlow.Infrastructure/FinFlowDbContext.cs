using System.Reflection.Emit;
using FinFlow.Domain.Entities;
using FinFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Infrastructure
{
    public class FinFlowDbContext : DbContext
    {
       public FinFlowDbContext(DbContextOptions<FinFlowDbContext> options):base(options) { }

        public virtual DbSet<BankAccount> Bankaccounts { get; set; }

        public virtual DbSet<BillReminder> Billreminders { get; set; }

        public virtual DbSet<Budget> Budgets { get; set; }

        public virtual DbSet<BudgetVsActual> BudgetVsActuals { get; set; }

        public virtual DbSet<BudgetCategory> Budgetcategories { get; set; }

        public virtual DbSet<BudgetPeriod> Budgetperiods { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<FinancialGoal> Financialgoals { get; set; }

        public virtual DbSet<Investment> Investments { get; set; }

        public virtual DbSet<InvestmentTransaction> Investmenttransactions { get; set; }

        public virtual DbSet<InvestmentType> Investmenttypes { get; set; }

        public virtual DbSet<MonthlySpendingSummary> MonthlySpendingSummaries { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<NotificationType> Notificationtypes { get; set; }

        public virtual DbSet<PlaidAccount> Plaidaccounts { get; set; }

        public virtual DbSet<Portfolio> Portfolios { get; set; }

        public virtual DbSet<PortfolioPerformance> PortfolioPerformances { get; set; }

        public virtual DbSet<RecurringTransaction> Recurringtransactions { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<SpendingPattern> Spendingpatterns { get; set; }

        public virtual DbSet<StockPrice> Stockprices { get; set; }

        public virtual DbSet<TaxCategory> Taxcategories { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TransactionStatus> Transactionstatuses { get; set; }

        public virtual DbSet<TransactionTag> Transactiontags { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserProfile> Userprofiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
           .HasPostgresEnum("budget_period_enum", new[] { "weekly", "monthly", "quarterly", "yearly" })
           .HasPostgresEnum("financial_experience_enum", new[] { "beginner", "intermediate", "advanced", "expert" })
           .HasPostgresEnum("goal_type_enum", new[] { "savings", "investment", "debt_payoff", "purchase", "emergency_fund" })
           .HasPostgresEnum("notification_priority_enum", new[] { "low", "medium", "high", "urgent" })
           .HasPostgresEnum("risk_tolerance_enum", new[] { "conservative", "moderate", "aggressive" })
           .HasPostgresEnum("transaction_status_enum", new[] { "pending", "completed", "failed", "cancelled" })
           .HasPostgresExtension("pg_stat_statements")
           .HasPostgresExtension("pgcrypto")
           .HasPostgresExtension("uuid-ossp");

            BudgetVsActualView(builder);
            MonthlySpendingSummaryView(builder);
            PortfolioPerformanceView(builder);
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyConfigurationsFromAssembly(typeof(FinFlowDbContext).Assembly);
        }

        private  void BudgetVsActualView(ModelBuilder builder)
        {
            builder.Entity<BudgetVsActual>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("budget_vs_actual");

                entity.Property(e => e.AllocatedAmount)
                    .HasPrecision(15, 2)
                    .HasColumnName("allocated_amount");
                entity.Property(e => e.BudgetId).HasColumnName("budget_id");
                entity.Property(e => e.BudgetName)
                    .HasMaxLength(255)
                    .HasColumnName("budget_name");
                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.IsOverBudget).HasColumnName("is_over_budget");
                entity.Property(e => e.PercentageUsed).HasColumnName("percentage_used");
                entity.Property(e => e.RemainingAmount)
                    .HasPrecision(15, 2)
                    .HasColumnName("remaining_amount");
                entity.Property(e => e.SpentAmount)
                    .HasPrecision(15, 2)
                    .HasColumnName("spent_amount");
                entity.Property(e => e.UserName).HasColumnName("user_name");
            });
        }
        private  void MonthlySpendingSummaryView(ModelBuilder builder)
        {
            builder.Entity<MonthlySpendingSummary>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("monthly_spending_summary");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.Month).HasColumnName("month");
                entity.Property(e => e.TotalIncome).HasColumnName("total_income");
                entity.Property(e => e.TotalSpent).HasColumnName("total_spent");
                entity.Property(e => e.TransactionCount).HasColumnName("transaction_count");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName).HasColumnName("user_name");
            });
        }

        private void PortfolioPerformanceView(ModelBuilder builder)
        {

            builder.Entity<PortfolioPerformance>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("portfolio_performance");

                entity.Property(e => e.AvgDailyChange).HasColumnName("avg_daily_change");
                entity.Property(e => e.InvestmentCount).HasColumnName("investment_count");
                entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");
                entity.Property(e => e.PortfolioName)
                    .HasMaxLength(255)
                    .HasColumnName("portfolio_name");
                entity.Property(e => e.TotalReturn)
                    .HasPrecision(15, 2)
                    .HasColumnName("total_return");
                entity.Property(e => e.TotalReturnPercent)
                    .HasPrecision(8, 4)
                    .HasColumnName("total_return_percent");
                entity.Property(e => e.TotalValue)
                    .HasPrecision(15, 2)
                    .HasColumnName("total_value");
                entity.Property(e => e.UserName).HasColumnName("user_name");
            });
        }

    }
}
