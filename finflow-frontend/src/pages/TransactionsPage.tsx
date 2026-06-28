import TransactionsTable from "@/components/transactions/TransactionsTable";
import { useTransactionModal } from "@/context/TransactionModalContext";
import { mockTransactions } from "@/data/mock";
import { Button } from "@/components/ui/button";
import { Icon } from "@/components/ui/icon";
import { Input } from "@/components/ui/input";

export default function TransactionsPage() {
  const { openModal } = useTransactionModal();

  return (
    <div className="space-y-8 p-4 md:p-8">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h2 className="text-3xl font-black tracking-tight">Transactions</h2>
          <p className="text-sm text-text-secondary">
            Manage and monitor your financial activity across all accounts.
          </p>
        </div>
        <Button onClick={openModal}>
          <Icon name="add_circle" />
          Add Transaction
        </Button>
      </div>

      <div className="flex flex-wrap gap-4">
        <Input placeholder="Search transactions..." className="max-w-xs border-none bg-card-dark" />
        <select className="h-11 rounded-lg border border-border-dark bg-card-dark px-4 text-sm">
          <option>All categories</option>
          <option>Income</option>
          <option>Dining</option>
          <option>Utilities</option>
        </select>
        <select className="h-11 rounded-lg border border-border-dark bg-card-dark px-4 text-sm">
          <option>Last 30 days</option>
          <option>Last 90 days</option>
          <option>This year</option>
        </select>
      </div>

      <TransactionsTable transactions={mockTransactions} />
    </div>
  );
}
