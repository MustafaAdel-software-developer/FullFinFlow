using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Infrastructure.Models;

public partial class FinFlowDbContext : DbContext
{
    public FinFlowDbContext()
    {
    }

    public FinFlowDbContext(DbContextOptions<FinFlowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bankaccount> Bankaccounts { get; set; }

    public virtual DbSet<Billreminder> Billreminders { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<BudgetVsActual> BudgetVsActuals { get; set; }

    public virtual DbSet<Budgetcategory> Budgetcategories { get; set; }

    public virtual DbSet<Budgetperiod> Budgetperiods { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Financialgoal> Financialgoals { get; set; }

    public virtual DbSet<Investment> Investments { get; set; }

    public virtual DbSet<Investmenttransaction> Investmenttransactions { get; set; }

    public virtual DbSet<Investmenttype> Investmenttypes { get; set; }

    public virtual DbSet<MonthlySpendingSummary> MonthlySpendingSummaries { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Notificationtype> Notificationtypes { get; set; }

    public virtual DbSet<Plaidaccount> Plaidaccounts { get; set; }

    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public virtual DbSet<PortfolioPerformance> PortfolioPerformances { get; set; }

    public virtual DbSet<Recurringtransaction> Recurringtransactions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Spendingpattern> Spendingpatterns { get; set; }

    public virtual DbSet<Stockprice> Stockprices { get; set; }

    public virtual DbSet<Taxcategory> Taxcategories { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transactionstatus> Transactionstatuses { get; set; }

    public virtual DbSet<Transactiontag> Transactiontags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FinFlowDb;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("budget_period_enum", new[] { "weekly", "monthly", "quarterly", "yearly" })
            .HasPostgresEnum("financial_experience_enum", new[] { "beginner", "intermediate", "advanced", "expert" })
            .HasPostgresEnum("goal_type_enum", new[] { "savings", "investment", "debt_payoff", "purchase", "emergency_fund" })
            .HasPostgresEnum("notification_priority_enum", new[] { "low", "medium", "high", "urgent" })
            .HasPostgresEnum("risk_tolerance_enum", new[] { "conservative", "moderate", "aggressive" })
            .HasPostgresEnum("transaction_status_enum", new[] { "pending", "completed", "failed", "cancelled" })
            .HasPostgresExtension("pg_stat_statements")
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Bankaccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bankaccounts_pkey");

            entity.ToTable("bankaccounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountName)
                .HasMaxLength(255)
                .HasColumnName("account_name");
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .HasColumnName("account_type");
            entity.Property(e => e.Balance)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("balance");
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .HasColumnName("bank_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.EncryptedAccountNumber)
                .HasComment("AES encrypted account number")
                .HasColumnName("encrypted_account_number");
            entity.Property(e => e.EncryptedRoutingNumber).HasColumnName("encrypted_routing_number");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PlaidAccountId)
                .HasMaxLength(255)
                .HasColumnName("plaid_account_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Bankaccounts)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("bankaccounts_currency_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bankaccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("bankaccounts_user_id_fkey");
        });

        modelBuilder.Entity<Billreminder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("billreminders_pkey");

            entity.ToTable("billreminders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BillName)
                .HasMaxLength(255)
                .HasColumnName("bill_name");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPaid)
                .HasDefaultValue(false)
                .HasColumnName("is_paid");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Billreminders)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("billreminders_category_id_fkey");

            entity.HasOne(d => d.Currency).WithMany(p => p.Billreminders)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("billreminders_currency_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Billreminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("billreminders_user_id_fkey");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("budgets_pkey");

            entity.ToTable("budgets", tb => tb.HasComment("User-defined budgets with time periods"));

            entity.HasIndex(e => e.IsActive, "idx_budgets_active").HasFilter("(is_active = true)");

            entity.HasIndex(e => e.UserId, "idx_budgets_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BudgetPeriodId).HasColumnName("budget_period_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(15, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.BudgetPeriod).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.BudgetPeriodId)
                .HasConstraintName("budgets_budget_period_id_fkey");

            entity.HasOne(d => d.Currency).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("budgets_currency_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Budgets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("budgets_user_id_fkey");
        });

        modelBuilder.Entity<BudgetVsActual>(entity =>
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

        modelBuilder.Entity<Budgetcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("budgetcategories_pkey");

            entity.ToTable("budgetcategories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllocatedAmount)
                .HasPrecision(15, 2)
                .HasColumnName("allocated_amount");
            entity.Property(e => e.BudgetId).HasColumnName("budget_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsOverBudget)
                .HasComputedColumnSql("(spent_amount > allocated_amount)", true)
                .HasColumnName("is_over_budget");
            entity.Property(e => e.RemainingAmount)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("(allocated_amount - spent_amount)", true)
                .HasColumnName("remaining_amount");
            entity.Property(e => e.SpentAmount)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("spent_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Budget).WithMany(p => p.Budgetcategories)
                .HasForeignKey(d => d.BudgetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("budgetcategories_budget_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Budgetcategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("budgetcategories_category_id_fkey");
        });

        modelBuilder.Entity<Budgetperiod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("budgetperiods_pkey");

            entity.ToTable("budgetperiods");

            entity.HasIndex(e => e.Name, "budgetperiods_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DaysInPeriod).HasColumnName("days_in_period");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#3498db'::character varying")
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsIncome)
                .HasDefaultValue(false)
                .HasColumnName("is_income");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("categories_parent_category_id_fkey");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("currencies_pkey");

            entity.ToTable("currencies");

            entity.HasIndex(e => e.Code, "currencies_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExchangeRate)
                .HasPrecision(10, 6)
                .HasDefaultValueSql("1.0")
                .HasColumnName("exchange_rate");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Symbol)
                .HasMaxLength(10)
                .HasColumnName("symbol");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Financialgoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("financialgoals_pkey");

            entity.ToTable("financialgoals", tb => tb.HasComment("User financial goals with progress tracking"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentAmount)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("current_amount");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValue(false)
                .HasColumnName("is_completed");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TargetAmount)
                .HasPrecision(15, 2)
                .HasColumnName("target_amount");
            entity.Property(e => e.TargetDate).HasColumnName("target_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Financialgoals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("financialgoals_user_id_fkey");
        });

        modelBuilder.Entity<Investment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("investments_pkey");

            entity.ToTable("investments", tb => tb.HasComment("Investment holdings with current market values"));

            entity.HasIndex(e => e.PortfolioId, "idx_investments_portfolio");

            entity.HasIndex(e => e.Symbol, "idx_investments_symbol");

            entity.HasIndex(e => e.UserId, "idx_investments_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(255)
                .HasColumnName("account_number");
            entity.Property(e => e.Broker)
                .HasMaxLength(255)
                .HasColumnName("broker");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentPrice)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("current_price");
            entity.Property(e => e.CurrentValue)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("(quantity * current_price)", true)
                .HasComment("Calculated as quantity * current_price")
                .HasColumnName("current_value");
            entity.Property(e => e.DayChange)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("day_change");
            entity.Property(e => e.DayChangePercent)
                .HasPrecision(8, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("day_change_percent");
            entity.Property(e => e.InvestmentTypeId).HasColumnName("investment_type_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PortfolioId).HasColumnName("portfolio_id");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.PurchasePrice)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("purchase_price");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 6)
                .HasDefaultValueSql("0.00")
                .HasColumnName("quantity");
            entity.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
            entity.Property(e => e.TotalReturn)
                .HasPrecision(15, 2)
                .HasComputedColumnSql("((current_price - purchase_price) * quantity)", true)
                .HasComment("Calculated as (current_price - purchase_price) * quantity")
                .HasColumnName("total_return");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.InvestmentType).WithMany(p => p.Investments)
                .HasForeignKey(d => d.InvestmentTypeId)
                .HasConstraintName("investments_investment_type_id_fkey");

            entity.HasOne(d => d.Portfolio).WithMany(p => p.Investments)
                .HasForeignKey(d => d.PortfolioId)
                .HasConstraintName("investments_portfolio_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Investments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("investments_user_id_fkey");
        });

        modelBuilder.Entity<Investmenttransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("investmenttransactions_pkey");

            entity.ToTable("investmenttransactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Fees)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("fees");
            entity.Property(e => e.InvestmentId).HasColumnName("investment_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Price)
                .HasPrecision(15, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasPrecision(15, 6)
                .HasColumnName("quantity");
            entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Investment).WithMany(p => p.Investmenttransactions)
                .HasForeignKey(d => d.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("investmenttransactions_investment_id_fkey");
        });

        modelBuilder.Entity<Investmenttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("investmenttypes_pkey");

            entity.ToTable("investmenttypes");

            entity.HasIndex(e => e.Name, "investmenttypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MonthlySpendingSummary>(entity =>
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

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications", tb => tb.HasComment("System notifications for users"));

            entity.HasIndex(e => e.IsRead, "idx_notifications_unread").HasFilter("(is_read = false)");

            entity.HasIndex(e => e.UserId, "idx_notifications_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(500)
                .HasColumnName("action_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("notifications_notification_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<Notificationtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notificationtypes_pkey");

            entity.ToTable("notificationtypes");

            entity.HasIndex(e => e.Name, "notificationtypes_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .HasColumnName("icon");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Plaidaccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plaidaccounts_pkey");

            entity.ToTable("plaidaccounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EncryptedPlaidAccessToken)
                .HasComment("AES encrypted Plaid access token")
                .HasColumnName("encrypted_plaid_access_token");
            entity.Property(e => e.InstitutionId)
                .HasMaxLength(255)
                .HasColumnName("institution_id");
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(255)
                .HasColumnName("institution_name");
            entity.Property(e => e.IsConnected)
                .HasDefaultValue(true)
                .HasColumnName("is_connected");
            entity.Property(e => e.LastSync)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_sync");
            entity.Property(e => e.PlaidAccountId)
                .HasMaxLength(255)
                .HasColumnName("plaid_account_id");
            entity.Property(e => e.PlaidItemId)
                .HasMaxLength(255)
                .HasColumnName("plaid_item_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.BankAccount).WithMany(p => p.Plaidaccounts)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plaidaccounts_bank_account_id_fkey");
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("portfolios_pkey");

            entity.ToTable("portfolios", tb => tb.HasComment("Collections of investments for organization"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TotalReturn)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_return");
            entity.Property(e => e.TotalReturnPercent)
                .HasPrecision(8, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_return_percent");
            entity.Property(e => e.TotalValue)
                .HasPrecision(15, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("total_value");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("portfolios_user_id_fkey");
        });

        modelBuilder.Entity<PortfolioPerformance>(entity =>
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

        modelBuilder.Entity<Recurringtransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recurringtransactions_pkey");

            entity.ToTable("recurringtransactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastProcessedDate).HasColumnName("last_processed_date");
            entity.Property(e => e.NextDueDate).HasColumnName("next_due_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.BankAccount).WithMany(p => p.Recurringtransactions)
                .HasForeignKey(d => d.BankAccountId)
                .HasConstraintName("recurringtransactions_bank_account_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Recurringtransactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("recurringtransactions_category_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Recurringtransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recurringtransactions_user_id_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .HasColumnName("file_path");
            entity.Property(e => e.GeneratedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("generated_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Parameters)
                .HasColumnType("jsonb")
                .HasColumnName("parameters");
            entity.Property(e => e.ReportName)
                .HasMaxLength(255)
                .HasColumnName("report_name");
            entity.Property(e => e.ReportType)
                .HasMaxLength(100)
                .HasColumnName("report_type");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("reports_currency_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("reports_user_id_fkey");
        });

        modelBuilder.Entity<Spendingpattern>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spendingpatterns_pkey");

            entity.ToTable("spendingpatterns");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnalysisDate).HasColumnName("analysis_date");
            entity.Property(e => e.AverageAmount)
                .HasPrecision(15, 2)
                .HasColumnName("average_amount");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Confidence)
                .HasPrecision(5, 4)
                .HasDefaultValueSql("0.00")
                .HasColumnName("confidence");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .HasColumnName("frequency");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PatternType)
                .HasMaxLength(100)
                .HasColumnName("pattern_type");
            entity.Property(e => e.PredictedNextDate).HasColumnName("predicted_next_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Spendingpatterns)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("spendingpatterns_category_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Spendingpatterns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("spendingpatterns_user_id_fkey");
        });

        modelBuilder.Entity<Stockprice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stockprices_pkey");

            entity.ToTable("stockprices");

            entity.HasIndex(e => e.PriceDate, "idx_stock_prices_date");

            entity.HasIndex(e => e.Symbol, "idx_stock_prices_symbol");

            entity.HasIndex(e => new { e.Symbol, e.PriceDate }, "stockprices_symbol_price_date_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClosePrice)
                .HasPrecision(15, 2)
                .HasColumnName("close_price");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HighPrice)
                .HasPrecision(15, 2)
                .HasColumnName("high_price");
            entity.Property(e => e.LowPrice)
                .HasPrecision(15, 2)
                .HasColumnName("low_price");
            entity.Property(e => e.OpenPrice)
                .HasPrecision(15, 2)
                .HasColumnName("open_price");
            entity.Property(e => e.PriceDate).HasColumnName("price_date");
            entity.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
            entity.Property(e => e.Volume).HasColumnName("volume");
        });

        modelBuilder.Entity<Taxcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("taxcategories_pkey");

            entity.ToTable("taxcategories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeductible)
                .HasDefaultValue(false)
                .HasColumnName("is_deductible");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(50)
                .HasColumnName("tax_code");
            entity.Property(e => e.TaxRate)
                .HasPrecision(5, 4)
                .HasDefaultValueSql("0.0")
                .HasColumnName("tax_rate");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Taxcategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("taxcategories_category_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transactions", tb => tb.HasComment("Financial transactions linked to bank accounts"));

            entity.HasIndex(e => e.Amount, "idx_transactions_amount");

            entity.HasIndex(e => e.CategoryId, "idx_transactions_category");

            entity.HasIndex(e => e.TransactionDate, "idx_transactions_date");

            entity.HasIndex(e => e.BankAccountId, "idx_transactions_user_id");

            entity.HasIndex(e => e.TransactionId, "transactions_transaction_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(15, 2)
                .HasComment("Positive for income, negative for expenses")
                .HasColumnName("amount");
            entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsExcludedFromBudget)
                .HasDefaultValue(false)
                .HasColumnName("is_excluded_from_budget");
            entity.Property(e => e.IsRecurring)
                .HasDefaultValue(false)
                .HasColumnName("is_recurring");
            entity.Property(e => e.MerchantName)
                .HasMaxLength(255)
                .HasColumnName("merchant_name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .HasColumnName("transaction_id");
            entity.Property(e => e.TransactionStatusId).HasColumnName("transaction_status_id");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.BankAccount).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactions_bank_account_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("transactions_category_id_fkey");

            entity.HasOne(d => d.TransactionStatus).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionStatusId)
                .HasConstraintName("transactions_transaction_status_id_fkey");
        });

        modelBuilder.Entity<Transactionstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactionstatuses_pkey");

            entity.ToTable("transactionstatuses");

            entity.HasIndex(e => e.Name, "transactionstatuses_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .HasDefaultValueSql("'#000000'::character varying")
                .HasColumnName("color");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Transactiontag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactiontags_pkey");

            entity.ToTable("transactiontags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.TagName)
                .HasMaxLength(100)
                .HasColumnName("tag_name");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Transactiontags)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transactiontags_transaction_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Main user accounts for the personal finance application"));

            entity.HasIndex(e => e.IsActive, "idx_users_active").HasFilter("(is_active = true)");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasComment("BCrypt hashed password for security")
                .HasColumnName("password");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiry)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refresh_token_expiry");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userprofiles_pkey");

            entity.ToTable("userprofiles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.AnnualIncome)
                .HasPrecision(15, 2)
                .HasColumnName("annual_income");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Egypt'::character varying")
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(500)
                .HasColumnName("profile_picture");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("zip_code");

            entity.HasOne(d => d.User).WithMany(p => p.Userprofiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userprofiles_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
