import TransactionsTable from "@/components/transactions/TransactionsTable";
import { Icon } from "@/components/ui/icon";
import { ProgressBar } from "@/components/ui/progress-bar";
import {
  mockChartWeeks,
  mockDashboardStats,
  mockSavingsGoals,
  mockTransactions,
} from "@/data/mock";

function formatCurrency(value: number) {
  return `$${value.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
}

export default function DashboardPage() {
  const stats = mockDashboardStats;

  return (
    <div className="space-y-8 p-4 md:p-8">
      <div className="flex items-end justify-between">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Financial Overview</h2>
          <p className="mt-1 text-muted-foreground">
            Welcome back, {stats.userName}. Here&apos;s what&apos;s happening with your money.
          </p>
        </div>
        <button className="flex items-center gap-2 rounded-lg border border-border-dark px-3 py-1.5 text-sm font-medium transition-colors hover:bg-card-dark">
          <Icon name="calendar_today" className="text-sm" />
          Last 30 Days
        </button>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        <div className="flex min-h-[180px] flex-col justify-between rounded-xl bg-primary p-6 text-primary-foreground shadow-lg shadow-primary/10">
          <div className="space-y-2">
            <p className="text-sm font-semibold uppercase tracking-wider opacity-80">Total Balance</p>
            <h3 className="text-4xl font-bold leading-tight">{formatCurrency(stats.totalBalance)}</h3>
          </div>
          <div className="mt-6 flex items-center gap-2 text-sm font-medium">
            <Icon name="trending_up" className="text-sm" />
            {stats.balanceChange}
          </div>
        </div>

        <div className="flex min-h-[180px] flex-col justify-between rounded-xl border border-border-dark bg-card-dark p-6">
          <div className="flex items-start justify-between">
            <div>
              <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
                Monthly Income
              </p>
              <h3 className="mt-2 text-3xl font-bold">{formatCurrency(stats.monthlyIncome)}</h3>
            </div>
            <div className="rounded-lg bg-primary/10 p-2 text-primary">
              <Icon name="arrow_downward" />
            </div>
          </div>
          <div className="mt-4 flex items-center gap-1 text-sm font-medium text-primary">
            <Icon name="add" className="text-xs" />
            {stats.incomeChange}
          </div>
        </div>

        <div className="flex min-h-[180px] flex-col justify-between rounded-xl border border-border-dark bg-card-dark p-6">
          <div className="flex items-start justify-between">
            <div>
              <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
                Monthly Expenses
              </p>
              <h3 className="mt-2 text-3xl font-bold">{formatCurrency(stats.monthlyExpenses)}</h3>
            </div>
            <div className="rounded-lg bg-red-500/10 p-2 text-red-500">
              <Icon name="arrow_upward" />
            </div>
          </div>
          <div className="mt-4 flex items-center gap-1 text-sm font-medium text-red-500">
            <Icon name="remove" className="text-xs" />
            {stats.expenseChange}
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-6 lg:grid-cols-3">
        <div className="rounded-xl border border-border-dark bg-card-dark p-6 lg:col-span-2">
          <div className="mb-6 flex items-center justify-between">
            <h3 className="text-lg font-bold">Monthly Spending</h3>
            <div className="flex gap-4">
              <div className="flex items-center gap-2 text-xs text-muted-foreground">
                <span className="size-2 rounded-full bg-primary" />
                Income
              </div>
              <div className="flex items-center gap-2 text-xs text-muted-foreground">
                <span className="size-2 rounded-full bg-muted-foreground" />
                Expenses
              </div>
            </div>
          </div>
          <div className="flex h-64 items-end gap-3 px-2">
            {mockChartWeeks.map((week, i) => (
              <div
                key={i}
                className="relative flex-1 rounded-t bg-muted"
                style={{ height: `${week.expense}%` }}
              >
                <div
                  className="absolute bottom-0 w-full rounded-t bg-primary opacity-80 transition-all hover:opacity-100"
                  style={{ height: `${week.income}%` }}
                />
              </div>
            ))}
          </div>
          <div className="mt-4 flex justify-between px-2 text-xs font-medium text-muted-foreground">
            {["WK 1", "WK 2", "WK 3", "WK 4", "WK 5", "WK 6", "WK 7", "WK 8"].map((w) => (
              <span key={w}>{w}</span>
            ))}
          </div>
        </div>

        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <h3 className="mb-6 text-lg font-bold">Savings Goals</h3>
          <div className="space-y-6">
            {mockSavingsGoals.map((goal) => (
              <div key={goal.id} className="space-y-2">
                <div className="flex justify-between text-sm">
                  <span className="font-medium">{goal.name}</span>
                  <span className="text-muted-foreground">
                    {formatCurrency(goal.current)} / {formatCurrency(goal.target)}
                  </span>
                </div>
                <ProgressBar value={(goal.current / goal.target) * 100} />
              </div>
            ))}
          </div>
          <button className="mt-8 w-full rounded-lg border border-dashed border-border-dark py-2 text-sm text-muted-foreground transition-colors hover:border-primary hover:text-primary">
            + Add New Goal
          </button>
        </div>
      </div>

      <TransactionsTable transactions={mockTransactions.slice(0, 4)} showViewAll />
    </div>
  );
}
