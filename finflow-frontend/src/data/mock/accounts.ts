export type BankAccount = {
  id: string;
  name: string;
  type: string;
  balance: number;
  lastFour: string;
  bank: string;
  icon: string;
  color: string;
};

export const mockAccounts: BankAccount[] = [
  { id: "1", name: "Primary Checking", type: "Checking", balance: 8420.5, lastFour: "4829", bank: "Chase", icon: "account_balance", color: "bg-blue-500/10 text-blue-400" },
  { id: "2", name: "High Yield Savings", type: "Savings", balance: 32100, lastFour: "7731", bank: "Ally", icon: "savings", color: "bg-primary/10 text-primary" },
  { id: "3", name: "Travel Rewards", type: "Credit", balance: -1240.8, lastFour: "9012", bank: "Amex", icon: "credit_card", color: "bg-purple-500/10 text-purple-400" },
];

export const mockAccountsTotal = 124592;
