export type FinancialGoal = {
  id: string;
  name: string;
  subtitle: string;
  current: number;
  target: number;
  icon: string;
  iconColor: string;
  deadline?: string;
};

export const mockGoals: FinancialGoal[] = [
  { id: "1", name: "Emergency Fund", subtitle: "Safety Net", current: 6500, target: 10000, icon: "shield", iconColor: "bg-blue-500/10 text-blue-500" },
  { id: "2", name: "New Car", subtitle: "Transportation", current: 8500, target: 15000, icon: "directions_car", iconColor: "bg-primary/10 text-primary" },
  { id: "3", name: "Vacation", subtitle: "Travel", current: 1200, target: 3000, icon: "flight", iconColor: "bg-amber-500/10 text-amber-500" },
  { id: "4", name: "Home Down Payment", subtitle: "Real Estate", current: 22000, target: 50000, icon: "home", iconColor: "bg-rose-500/10 text-rose-500" },
];

export const mockGoalsSummary = {
  totalSaved: 15500,
  activeGoals: 4,
  avgProgress: 45,
};
