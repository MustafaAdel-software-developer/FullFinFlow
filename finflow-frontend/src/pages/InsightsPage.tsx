import { mockInsightBudgets, mockInsights } from "@/data/mock/insights";
import { ProgressBar } from "@/components/ui/progress-bar";
import { Icon } from "@/components/ui/icon";

export default function InsightsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div>
        <h2 className="text-2xl font-bold tracking-tight">Financial Overview</h2>
        <p className="text-text-secondary">AI-powered insights into your spending habits.</p>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        {mockInsights.map((metric) => (
          <div key={metric.label} className="rounded-xl border border-border-dark bg-card-dark p-6">
            <p className="text-sm text-text-secondary">{metric.label}</p>
            <p className="mt-2 text-3xl font-bold">{metric.value}</p>
            <p className={`mt-2 text-sm font-medium ${metric.positive ? "text-primary" : "text-red-500"}`}>
              {metric.change}
            </p>
          </div>
        ))}
      </div>

      <div className="rounded-xl border border-border-dark bg-card-dark p-6">
        <h3 className="mb-6 text-xl font-bold">Budget Progress</h3>
        <div className="space-y-6">
          {mockInsightBudgets.map((b) => (
            <div key={b.name} className="space-y-2">
              <div className="flex justify-between text-sm">
                <span className="font-medium">{b.name}</span>
                <span className="text-text-secondary">{b.percent}% used</span>
              </div>
              <ProgressBar value={b.percent} />
            </div>
          ))}
        </div>
      </div>

      <div className="rounded-xl border border-primary/20 bg-primary/5 p-6">
        <div className="flex items-start gap-4">
          <Icon name="lightbulb" className="text-primary" />
          <div>
            <h4 className="font-bold">Tip of the week</h4>
            <p className="mt-2 text-sm text-text-secondary">
              Your dining expenses are 15% higher than last month. Consider setting a stricter budget
              for restaurants.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
