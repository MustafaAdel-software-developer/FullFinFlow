export type Budget = {
  id: string;
  name: string;
  spent: number;
  limit: number;
  category: string;
  icon: string;
};

export const mockBudgets: Budget[] = [
  { id: "1", name: "Food & Dining", spent: 420, limit: 600, category: "Essential", icon: "restaurant" },
  { id: "2", name: "Transportation", spent: 180, limit: 250, category: "Essential", icon: "directions_car" },
  { id: "3", name: "Entertainment", spent: 95, limit: 150, category: "Lifestyle", icon: "movie" },
  { id: "4", name: "Shopping", spent: 310, limit: 400, category: "Lifestyle", icon: "shopping_bag" },
  { id: "5", name: "Utilities", spent: 156, limit: 200, category: "Essential", icon: "bolt" },
];

export const mockBudgetSummary = {
  totalBudget: 1600,
  totalSpent: 1161,
  remaining: 439,
};
