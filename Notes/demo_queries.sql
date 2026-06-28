-- Personal Finance Tracker - Demo Queries
-- This file demonstrates the functionality of the database schema

-- ============================================================================
-- DEMO 1: User Dashboard Overview
-- ============================================================================

-- Get user's financial overview
SELECT 
    u.first_name || ' ' || u.last_name as user_name,
    up.annual_income,
    up.risk_tolerance,
    COUNT(DISTINCT ba.id) as total_accounts,
    SUM(ba.balance) as total_balance,
    COUNT(DISTINCT p.id) as total_portfolios,
    COUNT(DISTINCT fg.id) as total_goals
FROM users u
LEFT JOIN user_profiles up ON u.id = up.user_id
LEFT JOIN bank_accounts ba ON u.id = ba.user_id AND ba.is_active = TRUE
LEFT JOIN portfolios p ON u.id = p.user_id AND p.is_active = TRUE
LEFT JOIN financial_goals fg ON u.id = fg.user_id AND fg.is_completed = FALSE
WHERE u.email = 'john.doe@example.com'
GROUP BY u.id, u.first_name, u.last_name, up.annual_income, up.risk_tolerance;

-- ============================================================================
-- DEMO 2: Recent Transactions
-- ============================================================================

-- Get recent transactions with category details
SELECT 
    t.transaction_date,
    t.description,
    t.amount,
    c.name as category,
    c.color as category_color,
    ba.account_name,
    ts.name as status,
    ts.color as status_color
FROM transactions t
JOIN categories c ON t.category_id = c.id
JOIN bank_accounts ba ON t.bank_account_id = ba.id
JOIN transaction_statuses ts ON t.transaction_status_id = ts.id
JOIN users u ON ba.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND t.is_deleted = FALSE
ORDER BY t.transaction_date DESC, t.created_at DESC
LIMIT 10;

-- ============================================================================
-- DEMO 3: Budget vs Actual Spending
-- ============================================================================

-- Get current budget performance
SELECT 
    b.name as budget_name,
    c.name as category,
    bc.allocated_amount,
    bc.spent_amount,
    bc.remaining_amount,
    bc.is_over_budget,
    ROUND((bc.spent_amount / bc.allocated_amount * 100), 1) as percentage_used,
    CASE 
        WHEN bc.is_over_budget THEN '🔴 Over Budget'
        WHEN (bc.spent_amount / bc.allocated_amount) > 0.8 THEN '🟡 Near Limit'
        ELSE '🟢 On Track'
    END as status
FROM budgets b
JOIN budget_categories bc ON b.id = bc.budget_id
JOIN categories c ON bc.category_id = c.id
JOIN users u ON b.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND b.is_active = TRUE
ORDER BY bc.is_over_budget DESC, percentage_used DESC;

-- ============================================================================
-- DEMO 4: Investment Portfolio Performance
-- ============================================================================

-- Get portfolio performance with individual investments
SELECT 
    p.name as portfolio_name,
    i.symbol,
    i.name as investment_name,
    i.quantity,
    i.purchase_price,
    i.current_price,
    i.current_value,
    i.total_return,
    ROUND((i.total_return / (i.purchase_price * i.quantity) * 100), 2) as return_percentage,
    it.name as investment_type
FROM portfolios p
JOIN investments i ON p.id = i.portfolio_id
JOIN investment_types it ON i.investment_type_id = it.id
JOIN users u ON p.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND p.is_active = TRUE
  AND i.is_active = TRUE
ORDER BY p.name, i.total_return DESC;

-- ============================================================================
-- DEMO 5: Financial Goals Progress
-- ============================================================================

-- Get financial goals with progress
SELECT 
    fg.name as goal_name,
    fg.description,
    fg.target_amount,
    fg.current_amount,
    fg.target_date,
    fg.goal_type,
    ROUND((fg.current_amount / fg.target_amount * 100), 1) as progress_percentage,
    CASE 
        WHEN fg.current_amount >= fg.target_amount THEN '🎉 Completed!'
        WHEN (fg.current_amount / fg.target_amount) > 0.8 THEN '🟢 Almost There!'
        WHEN (fg.current_amount / fg.target_amount) > 0.5 THEN '🟡 Halfway There!'
        ELSE '🔵 Getting Started'
    END as progress_status,
    fg.target_date - CURRENT_DATE as days_remaining
FROM financial_goals fg
JOIN users u ON fg.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND fg.is_completed = FALSE
  AND fg.is_deleted = FALSE
ORDER BY progress_percentage DESC;

-- ============================================================================
-- DEMO 6: Monthly Spending Analysis
-- ============================================================================

-- Get monthly spending by category
SELECT 
    TO_CHAR(DATE_TRUNC('month', t.transaction_date), 'Month YYYY') as month,
    c.name as category,
    c.color as category_color,
    SUM(CASE WHEN t.amount < 0 THEN ABS(t.amount) ELSE 0 END) as total_spent,
    COUNT(*) as transaction_count,
    AVG(CASE WHEN t.amount < 0 THEN ABS(t.amount) ELSE NULL END) as avg_transaction_amount
FROM transactions t
JOIN categories c ON t.category_id = c.id
JOIN bank_accounts ba ON t.bank_account_id = ba.id
JOIN users u ON ba.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND t.is_deleted = FALSE
  AND t.amount < 0  -- Only expenses
  AND t.transaction_date >= CURRENT_DATE - INTERVAL '6 months'
GROUP BY DATE_TRUNC('month', t.transaction_date), c.name, c.color
ORDER BY month DESC, total_spent DESC;

-- ============================================================================
-- DEMO 7: Upcoming Bills and Recurring Payments
-- ============================================================================

-- Get upcoming bills and recurring payments
SELECT 
    'Bill Reminder' as type,
    br.bill_name as description,
    br.amount,
    br.due_date,
    c.name as category,
    br.frequency
FROM bill_reminders br
JOIN categories c ON br.category_id = c.id
JOIN users u ON br.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND br.is_active = TRUE
  AND br.due_date >= CURRENT_DATE
  AND br.due_date <= CURRENT_DATE + INTERVAL '30 days'

UNION ALL

SELECT 
    'Recurring Payment' as type,
    rt.description,
    rt.amount,
    rt.next_due_date as due_date,
    c.name as category,
    rt.frequency
FROM recurring_transactions rt
JOIN categories c ON rt.category_id = c.id
JOIN users u ON rt.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND rt.is_active = TRUE
  AND rt.next_due_date >= CURRENT_DATE
  AND rt.next_due_date <= CURRENT_DATE + INTERVAL '30 days'

ORDER BY due_date;

-- ============================================================================
-- DEMO 8: Unread Notifications
-- ============================================================================

-- Get unread notifications
SELECT 
    n.title,
    n.message,
    nt.name as notification_type,
    nt.icon,
    n.priority,
    n.created_at,
    n.action_url
FROM notifications n
JOIN notification_types nt ON n.notification_type_id = nt.id
JOIN users u ON n.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND n.is_read = FALSE
  AND n.is_deleted = FALSE
ORDER BY 
    CASE n.priority
        WHEN 'urgent' THEN 1
        WHEN 'high' THEN 2
        WHEN 'medium' THEN 3
        WHEN 'low' THEN 4
    END,
    n.created_at DESC;

-- ============================================================================
-- DEMO 9: Spending Patterns Analysis
-- ============================================================================

-- Get spending patterns for predictive analytics
SELECT 
    c.name as category,
    sp.average_amount,
    sp.frequency,
    sp.predicted_next_date,
    sp.confidence,
    sp.pattern_type,
    sp.analysis_date
FROM spending_patterns sp
JOIN categories c ON sp.category_id = c.id
JOIN users u ON sp.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND sp.is_active = TRUE
ORDER BY sp.confidence DESC, sp.average_amount DESC;

-- ============================================================================
-- DEMO 10: Tax Deductible Expenses
-- ============================================================================

-- Get tax deductible expenses for tax season
SELECT 
    t.transaction_date,
    t.description,
    t.amount,
    c.name as category,
    tc.tax_code,
    tc.tax_rate,
    tc.is_deductible,
    tc.description as tax_description
FROM transactions t
JOIN categories c ON t.category_id = c.id
JOIN tax_categories tc ON c.id = tc.category_id
JOIN bank_accounts ba ON t.bank_account_id = ba.id
JOIN users u ON ba.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND t.is_deleted = FALSE
  AND tc.is_deductible = TRUE
  AND t.transaction_date >= DATE_TRUNC('year', CURRENT_DATE)
ORDER BY t.transaction_date DESC;

-- ============================================================================
-- DEMO 11: Net Worth Calculation
-- ============================================================================

-- Calculate user's net worth
WITH assets AS (
    SELECT 
        SUM(ba.balance) as total_cash
    FROM bank_accounts ba
    JOIN users u ON ba.user_id = u.id
    WHERE u.email = 'john.doe@example.com'
      AND ba.is_active = TRUE
),
investments AS (
    SELECT 
        SUM(i.current_value) as total_investments
    FROM investments i
    JOIN users u ON i.user_id = u.id
    WHERE u.email = 'john.doe@example.com'
      AND i.is_active = TRUE
)
SELECT 
    a.total_cash as cash_and_savings,
    i.total_investments as investment_value,
    (a.total_cash + i.total_investments) as total_net_worth
FROM assets a, investments i;

-- ============================================================================
-- DEMO 12: Category Spending Trends
-- ============================================================================

-- Get spending trends by category over the last 6 months
SELECT 
    c.name as category,
    c.color,
    TO_CHAR(DATE_TRUNC('month', t.transaction_date), 'Mon YYYY') as month,
    SUM(CASE WHEN t.amount < 0 THEN ABS(t.amount) ELSE 0 END) as monthly_spending,
    COUNT(*) as transaction_count
FROM transactions t
JOIN categories c ON t.category_id = c.id
JOIN bank_accounts ba ON t.bank_account_id = ba.id
JOIN users u ON ba.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND t.is_deleted = FALSE
  AND t.amount < 0
  AND t.transaction_date >= CURRENT_DATE - INTERVAL '6 months'
GROUP BY c.name, c.color, DATE_TRUNC('month', t.transaction_date)
ORDER BY c.name, month;

-- ============================================================================
-- DEMO 13: Account Balance History
-- ============================================================================

-- Show account balances over time (simplified version)
SELECT 
    ba.account_name,
    ba.account_type,
    ba.balance as current_balance,
    ba.bank_name,
    c.name as currency,
    c.symbol as currency_symbol
FROM bank_accounts ba
JOIN currencies c ON ba.currency_id = c.id
JOIN users u ON ba.user_id = u.id
WHERE u.email = 'john.doe@example.com'
  AND ba.is_active = TRUE
ORDER BY ba.balance DESC;

-- ============================================================================
-- DEMO 14: Investment Transaction History
-- ============================================================================

-- Get investment transaction history
SELECT 
    i.symbol,
    i.name as investment_name,
    it.transaction_type,
    it.quantity,
    it.price,
    it.fees,
    it.transaction_date,
    it.notes
FROM investment_transactions it
JOIN investments i ON it.investment_id = i.id
JOIN users u ON i.user_id = u.id
WHERE u.email = 'john.doe@example.com'
ORDER BY it.transaction_date DESC, it.created_at DESC;

-- ============================================================================
-- DEMO 15: Budget Recommendations
-- ============================================================================

-- Generate budget recommendations based on spending patterns
WITH avg_spending AS (
    SELECT 
        c.id as category_id,
        c.name as category_name,
        AVG(ABS(t.amount)) as avg_monthly_spending
    FROM transactions t
    JOIN categories c ON t.category_id = c.id
    JOIN bank_accounts ba ON t.bank_account_id = ba.id
    JOIN users u ON ba.user_id = u.id
    WHERE u.email = 'john.doe@example.com'
      AND t.is_deleted = FALSE
      AND t.amount < 0
      AND t.transaction_date >= CURRENT_DATE - INTERVAL '6 months'
    GROUP BY c.id, c.name
)
SELECT 
    category_name,
    ROUND(avg_monthly_spending, 2) as avg_monthly_spending,
    ROUND(avg_monthly_spending * 1.1, 2) as recommended_budget,
    CASE 
        WHEN avg_monthly_spending > 1000 THEN 'High Priority'
        WHEN avg_monthly_spending > 500 THEN 'Medium Priority'
        ELSE 'Low Priority'
    END as priority
FROM avg_spending
ORDER BY avg_monthly_spending DESC; 