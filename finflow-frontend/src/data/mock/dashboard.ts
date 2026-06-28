export type TransactionStatus = "Completed" | "Pending" | "Failed";

export type Transaction = {
  id: string;
  name: string;
  category: string;
  date: string;
  amount: number;
  status: TransactionStatus;
  icon: string;
  isIncome?: boolean;
};

export const mockTransactions: Transaction[] = [
  { id: "1", name: "Apple Store", category: "Technology", date: "Oct 24, 2023", amount: -129, status: "Completed", icon: "shopping_cart" },
  { id: "2", name: "The Green Bistro", category: "Dining", date: "Oct 23, 2023", amount: -42.5, status: "Completed", icon: "restaurant" },
  { id: "3", name: "Monthly Salary", category: "Income", date: "Oct 20, 2023", amount: 5200, status: "Completed", icon: "payments", isIncome: true },
  { id: "4", name: "City Power & Light", category: "Utilities", date: "Oct 19, 2023", amount: -156.2, status: "Pending", icon: "bolt" },
  { id: "5", name: "Netflix", category: "Entertainment", date: "Oct 18, 2023", amount: -15.99, status: "Completed", icon: "movie" },
  { id: "6", name: "Whole Foods", category: "Groceries", date: "Oct 17, 2023", amount: -87.32, status: "Completed", icon: "shopping_basket" },
];

export type SavingsGoal = {
  id: string;
  name: string;
  current: number;
  target: number;
};

export const mockSavingsGoals: SavingsGoal[] = [
  { id: "1", name: "New Car", current: 8500, target: 15000 },
  { id: "2", name: "Emergency Fund", current: 5000, target: 5000 },
  { id: "3", name: "Vacation", current: 1200, target: 3000 },
];

export const mockDashboardStats = {
  userName: "Alex",
  totalBalance: 12450.32,
  balanceChange: "+2.5% from last month",
  monthlyIncome: 5200,
  incomeChange: "+5.2% increase",
  monthlyExpenses: 3150.4,
  expenseChange: "1.8% decrease",
};

export const mockChartWeeks = [
  { income: 60, expense: 40 },
  { income: 45, expense: 65 },
  { income: 75, expense: 55 },
  { income: 40, expense: 90 },
  { income: 85, expense: 45 },
  { income: 50, expense: 70 },
  { income: 90, expense: 30 },
  { income: 35, expense: 50 },
];
