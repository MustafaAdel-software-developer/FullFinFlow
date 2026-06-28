import { mockAccounts, mockAccountsTotal } from "@/data/mock/accounts";
import { Icon } from "@/components/ui/icon";
import { Button } from "@/components/ui/button";

function formatCurrency(v: number) {
  return `$${v.toLocaleString("en-US", { minimumFractionDigits: 2 })}`;
}

export default function AccountsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div>
        <p className="text-sm font-medium text-text-secondary">Total Net Worth</p>
        <h2 className="mt-2 text-4xl font-black tracking-tighter md:text-5xl">
          {formatCurrency(mockAccountsTotal)}
        </h2>
        <p className="mt-2 text-sm text-primary">+3.2% this month</p>
      </div>

      <div className="flex items-center justify-between">
        <h3 className="text-xl font-bold">Your Accounts</h3>
        <Button>
          <Icon name="add" />
          Link Account
        </Button>
      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
        {mockAccounts.map((account) => (
          <div
            key={account.id}
            className="rounded-xl border border-border-dark bg-card-dark p-6 transition-colors hover:border-primary/40"
          >
            <div className="flex items-start justify-between">
              <div className={`flex size-12 items-center justify-center rounded-xl ${account.color}`}>
                <Icon name={account.icon} />
              </div>
              <span className="text-xs text-muted-foreground">•••• {account.lastFour}</span>
            </div>
            <h4 className="mt-4 font-bold">{account.name}</h4>
            <p className="text-sm text-text-secondary">{account.bank} · {account.type}</p>
            <p className="mt-4 text-2xl font-bold">{formatCurrency(account.balance)}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
