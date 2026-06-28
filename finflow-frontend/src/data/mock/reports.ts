export type ReportCard = {
  id: string;
  title: string;
  description: string;
  value: string;
  change: string;
  icon: string;
};

export const mockReports: ReportCard[] = [
  { id: "1", title: "Monthly Summary", description: "Income vs expenses breakdown", value: "$2,049.60", change: "+8.2%", icon: "summarize" },
  { id: "2", title: "Spending by Category", description: "Top categories this month", value: "12 categories", change: "View details", icon: "pie_chart" },
  { id: "3", title: "Net Worth Trend", description: "3-month performance", value: "$124,592", change: "+4.1%", icon: "trending_up" },
  { id: "4", title: "Cash Flow", description: "Weekly inflow/outflow", value: "Positive", change: "+$1,200", icon: "waterfall_chart" },
];
