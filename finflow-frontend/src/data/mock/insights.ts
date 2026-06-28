export type InsightMetric = {
  label: string;
  value: string;
  change: string;
  positive: boolean;
};

export const mockInsights: InsightMetric[] = [
  { label: "Spending Score", value: "82/100", change: "+5 pts", positive: true },
  { label: "Savings Rate", value: "18%", change: "+2%", positive: true },
  { label: "Budget Adherence", value: "73%", change: "-4%", positive: false },
];

export const mockInsightBudgets = [
  { name: "Food & Dining", percent: 70 },
  { name: "Transportation", percent: 72 },
  { name: "Entertainment", percent: 63 },
  { name: "Shopping", percent: 78 },
];
