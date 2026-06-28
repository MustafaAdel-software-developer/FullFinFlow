export type Holding = {
  id: string;
  symbol: string;
  name: string;
  shares: number;
  price: number;
  change: number;
};

export const mockHoldings: Holding[] = [
  { id: "1", symbol: "AAPL", name: "Apple Inc.", shares: 25, price: 178.5, change: 2.4 },
  { id: "2", symbol: "MSFT", name: "Microsoft Corp.", shares: 15, price: 378.2, change: 1.1 },
  { id: "3", symbol: "VTI", name: "Vanguard Total Stock", shares: 40, price: 245.8, change: -0.3 },
  { id: "4", symbol: "BTC", name: "Bitcoin", shares: 0.5, price: 67200, change: 3.8 },
];

export const mockPortfolioAllocation = [
  { label: "Stocks", percent: 55, color: "bg-primary" },
  { label: "ETFs", percent: 30, color: "bg-blue-500" },
  { label: "Crypto", percent: 15, color: "bg-amber-500" },
];

export const mockPortfolioTotal = 48750.32;
