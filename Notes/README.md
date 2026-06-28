# Personal Finance Tracker Database

A comprehensive PostgreSQL database implementation for a personal finance management application, featuring advanced budgeting, investment tracking, goal management, and financial analytics.

## 🏗️ Database Architecture

This implementation includes:

- **20+ Tables** covering all aspects of personal finance
- **Advanced Security** with encrypted sensitive data
- **Multi-currency Support** for international users
- **Investment Portfolio Management** with real-time tracking
- **Automated Budgeting** with smart recommendations
- **Financial Goal Tracking** with progress visualization
- **Bill Reminders & Recurring Payments**
- **Tax Deduction Tracking**
- **Spending Pattern Analysis** for predictive insights
- **Comprehensive Reporting** system

## 📊 ERD Overview

The database follows a well-structured Entity-Relationship Design with:

- **Core Entities**: Users, Profiles, Currencies
- **Banking**: Accounts, Transactions, Categories, Plaid Integration
- **Budgeting**: Budgets, Categories, Periods, Tracking
- **Investments**: Portfolios, Holdings, Transactions, Market Data
- **Analytics**: Spending Patterns, Reports, Notifications
- **Automation**: Recurring Transactions, Bill Reminders

## 🚀 Quick Start

### Prerequisites

- PostgreSQL 12+ 
- `pgcrypto` extension (for encryption)
- `uuid-ossp` extension (for UUID generation)

### Installation

1. **Create Database**
   ```sql
   CREATE DATABASE personal_finance_tracker;
   ```

2. **Run Schema Setup**
   ```bash
   psql -d personal_finance_tracker -f database_schema.sql
   ```

3. **Test with Demo Queries**
   ```bash
   psql -d personal_finance_tracker -f demo_queries.sql
   ```

## 📁 File Structure

```
├── finance_tracker_erd.mermaid    # ERD diagram
├── database_schema.sql            # Complete database schema
├── demo_queries.sql              # Sample queries and reports
└── README.md                     # This file
```

## 🔧 Key Features

### 🔐 Security Features
- **Encrypted Sensitive Data**: Account numbers, routing numbers, Plaid tokens
- **Password Hashing**: BCrypt encryption for user passwords
- **Soft Deletes**: Data retention with logical deletion
- **Audit Trails**: Comprehensive timestamp tracking

### 💰 Multi-Currency Support
- Support for USD, EUR, GBP, JPY, CAD and more
- Exchange rate tracking
- Currency-specific budgets and reports

### 📈 Investment Management
- **Portfolio Organization**: Group investments by purpose
- **Real-time Tracking**: Current prices and performance
- **Transaction History**: Buy/sell records with fees
- **Performance Analytics**: Returns, percentages, trends

### 💳 Banking Integration
- **Plaid Integration**: Secure bank account linking
- **Transaction Categorization**: Smart category assignment
- **Account Aggregation**: Multiple bank support
- **Balance Tracking**: Real-time account balances

### 📊 Budgeting System
- **Flexible Periods**: Weekly, monthly, quarterly, yearly
- **Category Budgets**: Granular spending limits
- **Over-budget Alerts**: Real-time notifications
- **Smart Recommendations**: AI-powered budget suggestions

### 🎯 Goal Management
- **Multiple Goal Types**: Savings, investments, debt payoff
- **Progress Tracking**: Visual progress indicators
- **Milestone Celebrations**: Achievement notifications
- **Target Date Management**: Deadline tracking

### 🔔 Automation Features
- **Recurring Transactions**: Subscription tracking
- **Bill Reminders**: Payment due notifications
- **Smart Notifications**: Priority-based alerts
- **Pattern Recognition**: Spending behavior analysis

## 📊 Sample Data Included

The schema includes comprehensive sample data:

- **Sample User**: John Doe with complete profile
- **Bank Accounts**: Checking and savings accounts
- **Transactions**: Various expense and income transactions
- **Investments**: Apple, Microsoft, and ETF holdings
- **Budgets**: Monthly budget with category allocations
- **Goals**: Emergency fund and vacation savings
- **Bills**: Rent and insurance reminders
- **Notifications**: Budget alerts and goal milestones

## 🔍 Demo Queries

The `demo_queries.sql` file includes 15 comprehensive queries demonstrating:

1. **User Dashboard Overview** - Financial summary
2. **Recent Transactions** - Latest activity
3. **Budget vs Actual** - Spending analysis
4. **Portfolio Performance** - Investment returns
5. **Goal Progress** - Achievement tracking
6. **Monthly Spending** - Category analysis
7. **Upcoming Bills** - Payment reminders
8. **Notifications** - Unread alerts
9. **Spending Patterns** - Predictive analytics
10. **Tax Deductions** - Tax season preparation
11. **Net Worth** - Total asset calculation
12. **Spending Trends** - Historical analysis
13. **Account Balances** - Current positions
14. **Investment History** - Transaction records
15. **Budget Recommendations** - AI suggestions

## 🛠️ Advanced Features

### Stored Procedures
- `create_transaction()` - Secure transaction creation
- `update_investment_price()` - Real-time price updates

### Views
- `monthly_spending_summary` - Aggregated spending data
- `portfolio_performance` - Investment analytics
- `budget_vs_actual` - Budget tracking

### Triggers
- **Automatic Timestamps**: `updated_at` field management
- **Calculated Fields**: Real-time computed values
- **Data Integrity**: Constraint enforcement

## 📈 Performance Optimizations

- **Strategic Indexing**: Optimized for common queries
- **Partitioning Ready**: Designed for large datasets
- **Query Optimization**: Efficient joins and filters
- **Connection Pooling**: Ready for high concurrency

## 🔮 Future Enhancements

The schema is designed to support future features:

- **Machine Learning**: Spending prediction models
- **Advanced Analytics**: Custom reporting engine
- **Mobile Sync**: Real-time data synchronization
- **API Integration**: Third-party service connections
- **Multi-tenancy**: Business/enterprise features

## 🧪 Testing

To test the database functionality:

```sql
-- Test user creation
INSERT INTO users (email, password_hash, first_name, last_name) 
VALUES ('test@example.com', crypt('testpass', gen_salt('bf')), 'Test', 'User');

-- Test transaction creation
SELECT create_transaction(1, 5, -50.00, 'Test transaction', CURRENT_DATE, 'Test Merchant');

-- Test investment price update
SELECT update_investment_price(1, 180.00);
```

## 📚 Documentation

### Table Relationships
- **One-to-Many**: User → Bank Accounts, Budgets, Investments
- **Many-to-Many**: Transactions ↔ Categories (via tags)
- **Self-Referencing**: Categories (parent-child relationships)
- **Polymorphic**: Notifications (various entity types)

### Data Types
- **Monetary**: `DECIMAL(15,2)` for precision
- **Timestamps**: `TIMESTAMP` with automatic updates
- **Enums**: Type-safe status and category fields
- **JSONB**: Flexible parameter storage

### Constraints
- **Foreign Keys**: Referential integrity
- **Unique Constraints**: Email, transaction IDs
- **Check Constraints**: Amount validation
- **Generated Columns**: Computed values

## 🤝 Contributing

To extend the database:

1. **Add New Tables**: Follow the established naming conventions
2. **Update ERD**: Modify the Mermaid diagram
3. **Add Sample Data**: Include test data for new features
4. **Create Queries**: Add demo queries for new functionality
5. **Update Documentation**: Maintain comprehensive docs

## 📄 License

This database schema is provided as-is for educational and development purposes.

## 🆘 Support

For questions or issues:
1. Review the demo queries for usage examples
2. Check the ERD diagram for relationship understanding
3. Examine the sample data for implementation patterns

---

**Happy Financial Tracking! 💰📊** 