import { mockReports } from "@/data/mock/reports";
import { Icon } from "@/components/ui/icon";
import { Button } from "@/components/ui/button";

export default function ReportsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h2 className="text-3xl font-black tracking-tight">Financial Reports</h2>
          <p className="text-sm text-text-secondary">Analytics and insights into your financial health.</p>
        </div>
        <Button variant="outline">
          <Icon name="download" />
          Export
        </Button>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
        {mockReports.map((report) => (
          <div
            key={report.id}
            className="rounded-xl border border-border-dark bg-card-dark p-6 transition-colors hover:border-primary/40"
          >
            <div className="flex items-start gap-4">
              <div className="flex size-12 items-center justify-center rounded-xl bg-primary/10 text-primary">
                <Icon name={report.icon} />
              </div>
              <div className="flex-1">
                <h3 className="font-bold">{report.title}</h3>
                <p className="mt-1 text-sm text-text-secondary">{report.description}</p>
                <p className="mt-4 text-2xl font-bold">{report.value}</p>
                <p className="mt-1 text-sm text-primary">{report.change}</p>
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="rounded-xl border border-border-dark bg-card-dark p-6">
        <h3 className="mb-4 text-lg font-bold">Spending Trend</h3>
        <div className="flex h-48 items-end gap-2">
          {[40, 65, 55, 80, 45, 70, 50, 90, 60, 75, 55, 85].map((h, i) => (
            <div key={i} className="flex-1 rounded-t bg-primary/60" style={{ height: `${h}%` }} />
          ))}
        </div>
        <div className="mt-4 flex justify-between text-xs text-muted-foreground">
          {["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"].map((m) => (
            <span key={m}>{m}</span>
          ))}
        </div>
      </div>
    </div>
  );
}
