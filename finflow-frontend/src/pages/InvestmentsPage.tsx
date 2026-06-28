import { mockHoldings, mockPortfolioAllocation, mockPortfolioTotal } from "@/data/mock/investments";

function formatCurrency(v: number) {
  return `$${v.toLocaleString("en-US", { minimumFractionDigits: 2 })}`;
}

export default function InvestmentsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div>
        <h2 className="text-3xl font-black tracking-tight">Investment Portfolio</h2>
        <p className="text-text-secondary">Track your holdings and portfolio performance.</p>
        <p className="mt-4 text-4xl font-bold text-primary">{formatCurrency(mockPortfolioTotal)}</p>
      </div>

      <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <h3 className="mb-6 text-lg font-bold">Portfolio Allocation</h3>
          <div className="space-y-4">
            {mockPortfolioAllocation.map((item) => (
              <div key={item.label} className="flex items-center gap-4">
                <div className={`h-3 w-3 rounded-full ${item.color}`} />
                <span className="flex-1 text-sm">{item.label}</span>
                <span className="font-medium">{item.percent}%</span>
              </div>
            ))}
          </div>
          <div className="mt-6 flex h-4 overflow-hidden rounded-full">
            {mockPortfolioAllocation.map((item) => (
              <div key={item.label} className={item.color} style={{ width: `${item.percent}%` }} />
            ))}
          </div>
        </div>

        <div className="rounded-xl border border-border-dark bg-card-dark p-6">
          <h3 className="mb-4 text-lg font-bold">Holdings</h3>
          <div className="space-y-4">
            {mockHoldings.map((h) => (
              <div key={h.id} className="flex items-center justify-between border-b border-border-dark pb-4 last:border-0">
                <div>
                  <p className="font-bold">{h.symbol}</p>
                  <p className="text-xs text-text-secondary">{h.name}</p>
                </div>
                <div className="text-right">
                  <p className="font-medium">{formatCurrency(h.shares * h.price)}</p>
                  <p className={`text-xs ${h.change >= 0 ? "text-primary" : "text-red-500"}`}>
                    {h.change >= 0 ? "+" : ""}{h.change}%
                  </p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
