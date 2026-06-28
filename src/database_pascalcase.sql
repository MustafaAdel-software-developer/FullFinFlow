-- Enable necessary extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "pgcrypto";
CREATE EXTENSION IF NOT EXISTS "pg_stat_statements";

-- Create custom types
CREATE TYPE transaction_status_enum AS ENUM ('pending', 'completed', 'failed', 'cancelled');
CREATE TYPE budget_period_enum AS ENUM ('weekly', 'monthly', 'quarterly', 'yearly');
CREATE TYPE notification_priority_enum AS ENUM ('low', 'medium', 'high', 'urgent');
CREATE TYPE goal_type_enum AS ENUM ('savings', 'investment', 'debt_payoff', 'purchase', 'emergency_fund');
CREATE TYPE risk_tolerance_enum AS ENUM ('conservative', 'moderate', 'aggressive');
CREATE TYPE financial_experience_enum AS ENUM ('beginner', 'intermediate', 'advanced', 'expert');
-- ... existing code ...
-- Users table
CREATE TABLE Users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    refresh_token TEXT,
    refresh_token_expiry TIMESTAMP
);

-- User profiles
CREATE TABLE UserProfiles (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    profile_picture VARCHAR(500),
    phone_number VARCHAR(20),
    address TEXT,
    city VARCHAR(100),
    state VARCHAR(100),
    zip_code VARCHAR(20),
    country VARCHAR(100) DEFAULT 'Egypt',
    annual_income DECIMAL(15,2),
    date_of_birth DATE,
    risk_tolerance risk_tolerance_enum DEFAULT 'moderate',
    financial_experience financial_experience_enum DEFAULT 'beginner',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Currencies
CREATE TABLE Currencies (
    id SERIAL PRIMARY KEY,
    code VARCHAR(3) UNIQUE NOT NULL,
    name VARCHAR(100) NOT NULL,
    symbol VARCHAR(10) NOT NULL,
    exchange_rate DECIMAL(10,6) DEFAULT 1.0,
    is_default BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Bank accounts
CREATE TABLE BankAccounts (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    currency_id INTEGER REFERENCES Currencies(id),
    account_name VARCHAR(255) NOT NULL,
    account_type VARCHAR(50) NOT NULL,
    encrypted_account_number TEXT,
    encrypted_routing_number TEXT,
    balance DECIMAL(15,2) DEFAULT 0.00,
    bank_name VARCHAR(255) NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    plaid_account_id VARCHAR(255)
);

-- Plaid accounts
CREATE TABLE PlaidAccounts (
    id SERIAL PRIMARY KEY,
    bank_account_id INTEGER REFERENCES BankAccounts(id) ON DELETE CASCADE,
    plaid_item_id VARCHAR(255) NOT NULL,
    encrypted_plaid_access_token TEXT NOT NULL,
    plaid_account_id VARCHAR(255) NOT NULL,
    institution_id VARCHAR(255) NOT NULL,
    institution_name VARCHAR(255) NOT NULL,
    last_sync TIMESTAMP,
    is_connected BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Transaction statuses
CREATE TABLE TransactionStatuses (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL,
    description TEXT,
    color VARCHAR(7) DEFAULT '#000000',
    is_active BOOLEAN DEFAULT TRUE
);

-- Categories
CREATE TABLE Categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    color VARCHAR(7) DEFAULT '#3498db',
    icon VARCHAR(50),
    is_default BOOLEAN DEFAULT FALSE,
    is_income BOOLEAN DEFAULT FALSE,
    parent_category_id INTEGER REFERENCES Categories(id),
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Transactions
CREATE TABLE Transactions (
    id SERIAL PRIMARY KEY,
    bank_account_id INTEGER REFERENCES BankAccounts(id) ON DELETE CASCADE,
    category_id INTEGER REFERENCES Categories(id),
    transaction_status_id INTEGER REFERENCES TransactionStatuses(id),
    transaction_id VARCHAR(255) UNIQUE,
    amount DECIMAL(15,2) NOT NULL,
    description TEXT,
    transaction_date DATE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    merchant_name VARCHAR(255),
    transaction_type VARCHAR(50),
    is_recurring BOOLEAN DEFAULT FALSE,
    notes TEXT,
    is_excluded_from_budget BOOLEAN DEFAULT FALSE,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- Transaction tags
CREATE TABLE TransactionTags (
    id SERIAL PRIMARY KEY,
    transaction_id INTEGER REFERENCES Transactions(id) ON DELETE CASCADE,
    tag_name VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tax categories
CREATE TABLE TaxCategories (
    id SERIAL PRIMARY KEY,
    category_id INTEGER REFERENCES Categories(id) ON DELETE CASCADE,
    tax_code VARCHAR(50),
    tax_rate DECIMAL(5,4) DEFAULT 0.0,
    is_deductible BOOLEAN DEFAULT FALSE,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- ... existing code ...
-- ... existing code ...
-- Budget periods
CREATE TABLE BudgetPeriods (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL,
    description TEXT,
    days_in_period INTEGER NOT NULL,
    is_active BOOLEAN DEFAULT TRUE
);

-- Budgets
CREATE TABLE Budgets (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    currency_id INTEGER REFERENCES Currencies(id),
    name VARCHAR(255) NOT NULL,
    total_amount DECIMAL(15,2) NOT NULL,
    budget_period_id INTEGER REFERENCES BudgetPeriods(id),
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Budget categories
CREATE TABLE BudgetCategories (
    id SERIAL PRIMARY KEY,
    budget_id INTEGER REFERENCES Budgets(id) ON DELETE CASCADE,
    category_id INTEGER REFERENCES Categories(id),
    allocated_amount DECIMAL(15,2) NOT NULL,
    spent_amount DECIMAL(15,2) DEFAULT 0.00,
    remaining_amount DECIMAL(15,2) GENERATED ALWAYS AS (allocated_amount - spent_amount) STORED,
    is_over_budget BOOLEAN GENERATED ALWAYS AS (spent_amount > allocated_amount) STORED,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Investment types
CREATE TABLE InvestmentTypes (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    category VARCHAR(100),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Portfolios
CREATE TABLE Portfolios (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    total_value DECIMAL(15,2) DEFAULT 0.00,
    total_return DECIMAL(15,2) DEFAULT 0.00,
    total_return_percent DECIMAL(8,4) DEFAULT 0.00,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Investments
CREATE TABLE Investments (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    portfolio_id INTEGER REFERENCES Portfolios(id),
    investment_type_id INTEGER REFERENCES InvestmentTypes(id),
    symbol VARCHAR(20),
    name VARCHAR(255) NOT NULL,
    quantity DECIMAL(15,6) DEFAULT 0.00,
    purchase_price DECIMAL(15,2) DEFAULT 0.00,
    purchase_date DATE,
    current_price DECIMAL(15,2) DEFAULT 0.00,
    current_value DECIMAL(15,2) GENERATED ALWAYS AS (quantity * current_price) STORED,
    total_return DECIMAL(15,2) GENERATED ALWAYS AS ((current_price - purchase_price) * quantity) STORED,
    day_change DECIMAL(15,2) DEFAULT 0.00,
    day_change_percent DECIMAL(8,4) DEFAULT 0.00,
    account_number VARCHAR(255),
    broker VARCHAR(255),
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Investment transactions
CREATE TABLE InvestmentTransactions (
    id SERIAL PRIMARY KEY,
    investment_id INTEGER REFERENCES Investments(id) ON DELETE CASCADE,
    transaction_type VARCHAR(50) NOT NULL,
    quantity DECIMAL(15,6) NOT NULL,
    price DECIMAL(15,2) NOT NULL,
    fees DECIMAL(15,2) DEFAULT 0.00,
    transaction_date DATE NOT NULL,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Stock prices
CREATE TABLE StockPrices (
    id SERIAL PRIMARY KEY,
    symbol VARCHAR(20) NOT NULL,
    open_price DECIMAL(15,2),
    high_price DECIMAL(15,2),
    low_price DECIMAL(15,2),
    close_price DECIMAL(15,2) NOT NULL,
    volume BIGINT,
    price_date DATE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(symbol, price_date)
);

-- Spending patterns
CREATE TABLE SpendingPatterns (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    category_id INTEGER REFERENCES Categories(id),
    average_amount DECIMAL(15,2) NOT NULL,
    frequency VARCHAR(50) NOT NULL,
    predicted_next_date DATE,
    confidence DECIMAL(5,4) DEFAULT 0.00,
    analysis_date DATE NOT NULL,
    pattern_type VARCHAR(100),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Recurring transactions
CREATE TABLE RecurringTransactions (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    category_id INTEGER REFERENCES Categories(id),
    bank_account_id INTEGER REFERENCES BankAccounts(id),
    description TEXT NOT NULL,
    amount DECIMAL(15,2) NOT NULL,
    frequency VARCHAR(50) NOT NULL,
    next_due_date DATE NOT NULL,
    last_processed_date DATE,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Bill reminders
CREATE TABLE BillReminders (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    category_id INTEGER REFERENCES Categories(id),
    currency_id INTEGER REFERENCES Currencies(id),
    bill_name VARCHAR(255) NOT NULL,
    amount DECIMAL(15,2) NOT NULL,
    due_date DATE NOT NULL,
    frequency VARCHAR(50) NOT NULL,
    is_paid BOOLEAN DEFAULT FALSE,
    notes TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Financial goals
CREATE TABLE FinancialGoals (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    target_amount DECIMAL(15,2) NOT NULL,
    current_amount DECIMAL(15,2) DEFAULT 0.00,
    target_date DATE,
    goal_type goal_type_enum NOT NULL,
    is_completed BOOLEAN DEFAULT FALSE,
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Notification types
CREATE TABLE NotificationTypes (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    icon VARCHAR(50),
    is_active BOOLEAN DEFAULT TRUE
);

-- Notifications
CREATE TABLE Notifications (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    notification_type_id INTEGER REFERENCES NotificationTypes(id),
    title VARCHAR(255) NOT NULL,
    message TEXT NOT NULL,
    is_read BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    action_url VARCHAR(500),
    priority notification_priority_enum DEFAULT 'medium',
    is_deleted BOOLEAN DEFAULT FALSE
);

-- Reports
CREATE TABLE Reports (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES Users(id) ON DELETE CASCADE,
    currency_id INTEGER REFERENCES Currencies(id),
    report_type VARCHAR(100) NOT NULL,
    report_name VARCHAR(255) NOT NULL,
    file_path VARCHAR(500),
    generated_at TIMESTAMP,
    start_date DATE,
    end_date DATE,
    parameters JSONB,
    status VARCHAR(50) DEFAULT 'pending',
    is_deleted BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- ... existing code ...
-- ... existing code ...
-- User indexes
CREATE INDEX idx_users_email ON Users(email);
CREATE INDEX idx_users_active ON Users(is_active) WHERE is_active = TRUE;

-- Transaction indexes
CREATE INDEX idx_transactions_user_id ON Transactions(bank_account_id);
CREATE INDEX idx_transactions_date ON Transactions(transaction_date);
CREATE INDEX idx_transactions_category ON Transactions(category_id);
CREATE INDEX idx_transactions_amount ON Transactions(amount);

-- Budget indexes
CREATE INDEX idx_budgets_user_id ON Budgets(user_id);
CREATE INDEX idx_budgets_active ON Budgets(is_active) WHERE is_active = TRUE;

-- Investment indexes
CREATE INDEX idx_investments_user_id ON Investments(user_id);
CREATE INDEX idx_investments_portfolio ON Investments(portfolio_id);
CREATE INDEX idx_investments_symbol ON Investments(symbol);

-- Stock price indexes
CREATE INDEX idx_stock_prices_symbol ON StockPrices(symbol);
CREATE INDEX idx_stock_prices_date ON StockPrices(price_date);

-- Notification indexes
CREATE INDEX idx_notifications_user_id ON Notifications(user_id);
CREATE INDEX idx_notifications_unread ON Notifications(is_read) WHERE is_read = FALSE;

CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';
-- Triggers for data integrity
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON Users FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_user_profiles_updated_at BEFORE UPDATE ON UserProfiles FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_currencies_updated_at BEFORE UPDATE ON Currencies FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_bank_accounts_updated_at BEFORE UPDATE ON BankAccounts FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_plaid_accounts_updated_at BEFORE UPDATE ON PlaidAccounts FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_categories_updated_at BEFORE UPDATE ON Categories FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_transactions_updated_at BEFORE UPDATE ON Transactions FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_tax_categories_updated_at BEFORE UPDATE ON TaxCategories FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_budgets_updated_at BEFORE UPDATE ON Budgets FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_budget_categories_updated_at BEFORE UPDATE ON BudgetCategories FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_portfolios_updated_at BEFORE UPDATE ON Portfolios FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_investments_updated_at BEFORE UPDATE ON Investments FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_investment_transactions_updated_at BEFORE UPDATE ON InvestmentTransactions FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_spending_patterns_updated_at BEFORE UPDATE ON SpendingPatterns FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_recurring_transactions_updated_at BEFORE UPDATE ON RecurringTransactions FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_bill_reminders_updated_at BEFORE UPDATE ON BillReminders FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_financial_goals_updated_at BEFORE UPDATE ON FinancialGoals FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_reports_updated_at BEFORE UPDATE ON Reports FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- ... existing code ...
-- ... existing code ...
-- Insert default currencies
INSERT INTO Currencies (code, name, symbol, exchange_rate, is_default) VALUES
('USD', 'US Dollar', '$', 1.0, TRUE),
('EUR', 'Euro', '€', 0.85, FALSE),
('GBP', 'British Pound', '£', 0.73, FALSE),
('JPY', 'Japanese Yen', '¥', 110.0, FALSE),
('CAD', 'Canadian Dollar', 'C$', 1.25, FALSE);

-- Insert transaction statuses
INSERT INTO TransactionStatuses (name, description, color) VALUES
('pending', 'Transaction is pending processing', '#f39c12'),
('completed', 'Transaction has been completed', '#27ae60'),
('failed', 'Transaction failed to process', '#e74c3c'),
('cancelled', 'Transaction was cancelled', '#95a5a6');

-- Insert budget periods
INSERT INTO BudgetPeriods (name, description, days_in_period) VALUES
('weekly', 'Weekly budget cycle', 7),
('monthly', 'Monthly budget cycle', 30),
('quarterly', 'Quarterly budget cycle', 90),
('yearly', 'Yearly budget cycle', 365);

-- Insert investment types
INSERT INTO InvestmentTypes (name, description, category) VALUES
('Stock', 'Individual company stocks', 'Equity'),
('ETF', 'Exchange Traded Funds', 'Equity'),
('Mutual Fund', 'Mutual funds', 'Equity'),
('Bond', 'Government and corporate bonds', 'Fixed Income'),
('Crypto', 'Cryptocurrencies', 'Alternative'),
('Real Estate', 'Real estate investments', 'Alternative'),
('Commodity', 'Commodities like gold, silver', 'Alternative');

-- Insert notification types
INSERT INTO NotificationTypes (name, description, icon) VALUES
('budget_alert', 'Budget limit exceeded', 'warning'),
('bill_due', 'Bill payment reminder', 'calendar'),
('goal_milestone', 'Financial goal milestone reached', 'trophy'),
('investment_alert', 'Investment performance alert', 'chart'),
('security_alert', 'Security or fraud alert', 'shield'),
('system_notification', 'System maintenance or updates', 'info');

-- Insert default categories
INSERT INTO Categories (name, description, color, icon, is_default, is_income) VALUES
-- Income categories
('Salary', 'Regular employment income', '#27ae60', 'money', TRUE, TRUE),
('Freelance', 'Freelance or contract work', '#27ae60', 'briefcase', FALSE, TRUE),
('Investment Income', 'Dividends, interest, capital gains', '#27ae60', 'chart-line', FALSE, TRUE),
('Other Income', 'Miscellaneous income sources', '#27ae60', 'plus-circle', FALSE, TRUE),
-- Expense categories
('Food & Dining', 'Groceries, restaurants, takeout', '#e74c3c', 'utensils', TRUE, FALSE),
('Transportation', 'Gas, public transit, rideshare', '#f39c12', 'car', TRUE, FALSE),
('Housing', 'Rent, mortgage, utilities', '#3498db', 'home', TRUE, FALSE),
('Entertainment', 'Movies, games, hobbies', '#9b59b6', 'gamepad', TRUE, FALSE),
('Shopping', 'Clothing, electronics, general', '#e67e22', 'shopping-cart', TRUE, FALSE),
('Healthcare', 'Medical expenses, insurance', '#e74c3c', 'heartbeat', TRUE, FALSE),
('Education', 'Tuition, books, courses', '#3498db', 'graduation-cap', TRUE, FALSE),
('Travel', 'Vacations, business trips', '#1abc9c', 'plane', TRUE, FALSE),
('Utilities', 'Electricity, water, internet', '#95a5a6', 'bolt', TRUE, FALSE),
('Insurance', 'Health, auto, life insurance', '#34495e', 'shield-alt', TRUE, FALSE);

-- ... existing code ...
-- ... existing code ...
-- Insert sample user
INSERT INTO Users (email, password, first_name, last_name) VALUES
('john.doe@example.com', crypt('password123', gen_salt('bf')), 'John', 'Doe');

-- Insert user profile
INSERT INTO UserProfiles (user_id, phone_number, city, state, annual_income, risk_tolerance, financial_experience) VALUES
(1, '+1-555-0123', 'New York', 'NY', 75000.00, 'moderate', 'intermediate');

-- Insert sample bank account
INSERT INTO BankAccounts (user_id, currency_id, account_name, account_type, balance, bank_name) VALUES
(1, 1, 'Chase Checking', 'checking', 2500.00, 'Chase Bank'),
(1, 1, 'Chase Savings', 'savings', 15000.00, 'Chase Bank');

-- Insert sample portfolio
INSERT INTO Portfolios (user_id, name, description) VALUES
(1, 'Retirement Portfolio', 'Long-term retirement investments'),
(1, 'Trading Portfolio', 'Active trading account');

-- Insert sample investments
INSERT INTO Investments (user_id, portfolio_id, investment_type_id, symbol, name, quantity, purchase_price, current_price, broker) VALUES
(1, 1, 1, 'AAPL', 'Apple Inc.', 10, 150.00, 175.50, 'Fidelity'),
(1, 1, 1, 'MSFT', 'Microsoft Corporation', 5, 300.00, 325.75, 'Fidelity'),
(1, 2, 2, 'SPY', 'SPDR S&P 500 ETF', 20, 400.00, 415.25, 'Robinhood');

-- Insert sample budget
INSERT INTO Budgets (user_id, currency_id, name, total_amount, budget_period_id, start_date, end_date) VALUES
(1, 1, 'Monthly Budget - January 2024', 5000.00, 2, '2024-01-01', '2024-01-31');

-- Insert sample budget categories
INSERT INTO BudgetCategories (budget_id, category_id, allocated_amount) VALUES
(1, 5, 800.00),  -- Food & Dining
(1, 6, 400.00),  -- Transportation
(1, 7, 2000.00), -- Housing
(1, 8, 300.00),  -- Entertainment
(1, 9, 500.00);  -- Shopping

-- Insert sample financial goal
INSERT INTO FinancialGoals (user_id, name, description, target_amount, current_amount, target_date, goal_type) VALUES
(1, 'Emergency Fund', 'Save 6 months of expenses', 15000.00, 12000.00, '2024-06-01', 'emergency_fund'),
(1, 'Vacation Fund', 'Save for summer vacation', 5000.00, 2500.00, '2024-07-01', 'savings');

-- Insert sample transactions
INSERT INTO Transactions (bank_account_id, category_id, transaction_status_id, amount, description, transaction_date, merchant_name) VALUES
(1, 5, 2, -85.50, 'Grocery shopping', '2024-01-15', 'Whole Foods'),
(1, 6, 2, -45.00, 'Gas station', '2024-01-14', 'Shell'),
(1, 8, 2, -25.00, 'Movie tickets', '2024-01-13', 'AMC Theaters'),
(1, 1, 2, 3000.00, 'Salary deposit', '2024-01-01', 'Employer Inc.');

-- Insert sample bill reminder
INSERT INTO BillReminders (user_id, category_id, currency_id, bill_name, amount, due_date, frequency) VALUES
(1, 7, 1, 'Rent Payment', 2000.00, '2024-02-01', 'monthly'),
(1, 11, 1, 'Car Insurance', 150.00, '2024-02-15', 'monthly');

-- Insert sample recurring transaction
INSERT INTO RecurringTransactions (user_id, category_id, bank_account_id, description, amount, frequency, next_due_date) VALUES
(1, 5, 1, 'Netflix Subscription', -15.99, 'monthly', '2024-02-01'),
(1, 10, 1, 'Internet Bill', -89.99, 'monthly', '2024-02-01');

-- Insert sample notifications
INSERT INTO Notifications (user_id, notification_type_id, title, message, priority) VALUES
(1, 1, 'Budget Alert', 'You have exceeded your Food & Dining budget by $50', 'high'),
(1, 2, 'Bill Due Soon', 'Your rent payment of $2000 is due in 3 days', 'urgent'),
(1, 3, 'Goal Milestone', 'Congratulations! You are 80% to your Emergency Fund goal', 'medium');

-- Monthly spending summary view
CREATE VIEW monthly_spending_summary AS
SELECT 
    u.id as user_id,
    u.first_name || ' ' || u.last_name as user_name,
    DATE_TRUNC('month', t.transaction_date) as month,
    c.name as category,
    SUM(CASE WHEN t.amount < 0 THEN ABS(t.amount) ELSE 0 END) as total_spent,
    SUM(CASE WHEN t.amount > 0 THEN t.amount ELSE 0 END) as total_income,
    COUNT(*) as transaction_count
FROM Users u
JOIN BankAccounts ba ON u.id = ba.user_id
JOIN Transactions t ON ba.id = t.bank_account_id
JOIN Categories c ON t.category_id = c.id
WHERE t.is_deleted = FALSE AND ba.is_deleted = FALSE
GROUP BY u.id, u.first_name, u.last_name, DATE_TRUNC('month', t.transaction_date), c.name;

-- Portfolio performance view
CREATE VIEW portfolio_performance AS
SELECT 
    p.id as portfolio_id,
    p.name as portfolio_name,
    u.first_name || ' ' || u.last_name as user_name,
    p.total_value,
    p.total_return,
    p.total_return_percent,
    COUNT(i.id) as investment_count,
    AVG(i.day_change_percent) as avg_daily_change
FROM Portfolios p
JOIN Users u ON p.user_id = u.id
LEFT JOIN Investments i ON p.id = i.portfolio_id AND i.is_active = TRUE
WHERE p.is_deleted = FALSE
GROUP BY p.id, p.name, u.first_name, u.last_name, p.total_value, p.total_return, p.total_return_percent;

-- Budget vs actual spending view
CREATE VIEW budget_vs_actual AS
SELECT 
    b.id as budget_id,
    b.name as budget_name,
    u.first_name || ' ' || u.last_name as user_name,
    c.name as category,
    bc.allocated_amount,
    bc.spent_amount,
    bc.remaining_amount,
    bc.is_over_budget,
    ROUND((bc.spent_amount / bc.allocated_amount * 100), 2) as percentage_used
FROM Budgets b
JOIN Users u ON b.user_id = u.id
JOIN BudgetCategories bc ON b.id = bc.budget_id
JOIN Categories c ON bc.category_id = c.id
WHERE b.is_active = TRUE AND b.is_deleted = FALSE;

-- Procedure to create a new transaction and update account balance
CREATE OR REPLACE FUNCTION create_transaction(
    p_bank_account_id INTEGER,
    p_category_id INTEGER,
    p_amount DECIMAL,
    p_description TEXT,
    p_transaction_date DATE,
    p_merchant_name VARCHAR(255)
) RETURNS INTEGER AS $$
DECLARE
    v_transaction_id INTEGER;
BEGIN
    -- Insert transaction
    INSERT INTO Transactions (bank_account_id, category_id, transaction_status_id, amount, description, transaction_date, merchant_name)
    VALUES (p_bank_account_id, p_category_id, 2, p_amount, p_description, p_transaction_date, p_merchant_name)
    RETURNING id INTO v_transaction_id;
    
    -- Update account balance
    UPDATE BankAccounts 
    SET balance = balance + p_amount,
        updated_at = CURRENT_TIMESTAMP
    WHERE id = p_bank_account_id;
    
    RETURN v_transaction_id;
END;
$$ LANGUAGE plpgsql;

-- Procedure to update investment current price and recalculate values
CREATE OR REPLACE FUNCTION update_investment_price(
    p_investment_id INTEGER,
    p_current_price DECIMAL
) RETURNS VOID AS $$
BEGIN
    UPDATE Investments 
    SET current_price = p_current_price,
        updated_at = CURRENT_TIMESTAMP
    WHERE id = p_investment_id;
    
    -- Update portfolio totals
    UPDATE Portfolios 
    SET total_value = (
        SELECT COALESCE(SUM(current_value), 0)
        FROM Investments 
        WHERE portfolio_id = Portfolios.id AND is_active = TRUE
    ),
    total_return = (
        SELECT COALESCE(SUM(total_return), 0)
        FROM Investments 
        WHERE portfolio_id = Portfolios.id AND is_active = TRUE
    ),
    updated_at = CURRENT_TIMESTAMP
    WHERE id = (SELECT portfolio_id FROM Investments WHERE id = p_investment_id);
END;
$$ LANGUAGE plpgsql;

-- Comments for documentation
COMMENT ON TABLE Users IS 'Main user accounts for the personal finance application';
COMMENT ON TABLE Transactions IS 'Financial transactions linked to bank accounts';
COMMENT ON TABLE Budgets IS 'User-defined budgets with time periods';
COMMENT ON TABLE Investments IS 'Investment holdings with current market values';
COMMENT ON TABLE Portfolios IS 'Collections of investments for organization';
COMMENT ON TABLE FinancialGoals IS 'User financial goals with progress tracking';
COMMENT ON TABLE Notifications IS 'System notifications for users';

COMMENT ON COLUMN Users.password IS 'BCrypt hashed password for security';
COMMENT ON COLUMN BankAccounts.encrypted_account_number IS 'AES encrypted account number';
COMMENT ON COLUMN PlaidAccounts.encrypted_plaid_access_token IS 'AES encrypted Plaid access token';
COMMENT ON COLUMN Transactions.amount IS 'Positive for income, negative for expenses';
COMMENT ON COLUMN Investments.current_value IS 'Calculated as quantity * current_price';
COMMENT ON COLUMN Investments.total_return IS 'Calculated as (current_price - purchase_price) * quantity';
-- ... existing code ...