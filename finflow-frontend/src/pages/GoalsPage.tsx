import { mockGoals, mockGoalsSummary } from "@/data/mock/goals";
import { Icon } from "@/components/ui/icon";
import { ProgressBar } from "@/components/ui/progress-bar";
import { Button } from "@/components/ui/button";

function formatCurrency(v: number) {
  return `$${v.toLocaleString("en-US", { minimumFractionDigits: 0 })}`;
}

export default function GoalsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div className="flex flex-wrap items-end justify-between gap-4">
        <div>
          <h2 className="text-4xl font-black tracking-tight">Financial Goals</h2>
          <p className="text-text-secondary">Track your progress and reach your milestones faster.</p>
        </div>
        <Button size="lg">
          <Icon name="add_circle" />
          Create New Goal
        </Button>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Total Saved</p>
          <p className="mt-2 text-3xl font-bold">{formatCurrency(mockGoalsSummary.totalSaved)}</p>
        </div>
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Active Goals</p>
          <p className="mt-2 text-3xl font-bold">{mockGoalsSummary.activeGoals}</p>
        </div>
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <p className="text-sm text-text-secondary">Avg. Progress</p>
          <p className="mt-2 text-3xl font-bold text-primary">{mockGoalsSummary.avgProgress}%</p>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
        {mockGoals.map((goal) => {
          const pct = Math.round((goal.current / goal.target) * 100);
          return (
            <div
              key={goal.id}
              className="flex flex-col gap-6 rounded-xl border border-border-dark bg-card-dark p-6 transition-all hover:border-primary/40"
            >
              <div className="flex items-start justify-between">
                <div className="flex items-center gap-4">
                  <div className={`flex size-12 items-center justify-center rounded-xl ${goal.iconColor}`}>
                    <Icon name={goal.icon} className="text-3xl" />
                  </div>
                  <div>
                    <h3 className="text-lg font-bold">{goal.name}</h3>
                    <p className="text-xs text-text-secondary">{goal.subtitle}</p>
                  </div>
                </div>
                <Icon name="more_vert" className="text-muted-foreground" />
              </div>
              <div className="space-y-2">
                <div className="flex items-end justify-between">
                  <span className="text-sm text-text-secondary">Progress</span>
                  <span className="text-2xl font-black text-primary">{pct}%</span>
                </div>
                <ProgressBar value={pct} className="h-3" />
              </div>
              <div className="flex justify-between text-sm">
                <span>{formatCurrency(goal.current)} saved</span>
                <span className="text-text-secondary">of {formatCurrency(goal.target)}</span>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
