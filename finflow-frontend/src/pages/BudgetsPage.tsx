import { mockBudgets, mockBudgetSummary } from "@/data/mock/budgets";
import { Icon } from "@/components/ui/icon";
import { ProgressBar } from "@/components/ui/progress-bar";
import { Button } from "@/components/ui/button";

function formatCurrency(v: number) {
  return `$${v.toLocaleString("en-US", { minimumFractionDigits: 0 })}`;
}

export default function BudgetsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h2 className="text-3xl font-black tracking-tight">Budgets</h2>
          <p className="text-sm text-text-secondary">Track spending against your monthly limits.</p>
        </div>
        <Button>
          <Icon name="add" />
          Create Budget
        </Button>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Total Budget</p>
          <p className="mt-2 text-3xl font-bold">{formatCurrency(mockBudgetSummary.totalBudget)}</p>
        </div>
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Spent</p>
          <p className="mt-2 text-3xl font-bold">{formatCurrency(mockBudgetSummary.totalSpent)}</p>
        </div>
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Remaining</p>
          <p className="mt-2 text-3xl font-bold text-primary">{formatCurrency(mockBudgetSummary.remaining)}</p>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
        {mockBudgets.map((budget) => {
          const pct = (budget.spent / budget.limit) * 100;
          return (
            <div key={budget.id} className="rounded-xl border border-border-dark bg-card-dark p-6">
              <div className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                  <div className="flex size-10 items-center justify-center rounded-lg bg-primary/10 text-primary">
                    <Icon name={budget.icon} />
                  </div>
                  <div>
                    <h4 className="font-bold">{budget.name}</h4>
                    <p className="text-xs text-text-secondary">{budget.category}</p>
                  </div>
                </div>
                <span className="text-sm font-medium">{Math.round(pct)}%</span>
              </div>
              <div className="mt-4 space-y-2">
                <div className="flex justify-between text-sm text-text-secondary">
                  <span>{formatCurrency(budget.spent)} spent</span>
                  <span>{formatCurrency(budget.limit)} limit</span>
                </div>
                <ProgressBar value={pct} />
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
