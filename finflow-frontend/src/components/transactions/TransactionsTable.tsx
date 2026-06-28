import { Link } from "react-router-dom";
import type { Transaction } from "@/data/mock/dashboard";
import { Icon } from "@/components/ui/icon";
import { cn } from "@/lib/utils";

type TransactionsTableProps = {
  transactions: Transaction[];
  showViewAll?: boolean;
};

function formatAmount(amount: number) {
  const prefix = amount >= 0 ? "+" : "";
  return `${prefix}$${Math.abs(amount).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
}

export default function TransactionsTable({ transactions, showViewAll }: TransactionsTableProps) {
  return (
    <div className="stitch-card overflow-hidden">
      <div className="flex items-center justify-between border-b border-border-dark p-6">
        <h3 className="text-lg font-bold">Recent Transactions</h3>
        {showViewAll && (
          <Link to="/transactions" className="text-sm font-medium text-primary hover:underline">
            View All
          </Link>
        )}
      </div>
      <div className="overflow-x-auto">
        <table className="w-full text-left">
          <thead>
            <tr className="text-xs font-semibold uppercase tracking-wider text-muted-foreground">
              <th className="px-6 py-4">Transaction</th>
              <th className="px-6 py-4">Category</th>
              <th className="px-6 py-4">Date</th>
              <th className="px-6 py-4">Amount</th>
              <th className="px-6 py-4">Status</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-border-dark">
            {transactions.map((tx) => (
              <tr key={tx.id} className="transition-colors hover:bg-card-dark/50">
                <td className="px-6 py-4">
                  <div className="flex items-center gap-3">
                    <div className="flex size-8 items-center justify-center rounded bg-muted text-muted-foreground">
                      <Icon name={tx.icon} className="text-lg" />
                    </div>
                    <span className="text-sm font-medium">{tx.name}</span>
                  </div>
                </td>
                <td className="px-6 py-4">
                  <span
                    className={cn(
                      "rounded px-2 py-1 text-xs",
                      tx.isIncome
                        ? "bg-primary/10 text-primary"
                        : "bg-muted text-muted-foreground"
                    )}
                  >
                    {tx.category}
                  </span>
                </td>
                <td className="px-6 py-4 text-sm text-muted-foreground">{tx.date}</td>
                <td
                  className={cn(
                    "px-6 py-4 text-sm font-semibold",
                    tx.isIncome ? "text-primary" : ""
                  )}
                >
                  {formatAmount(tx.amount)}
                </td>
                <td className="px-6 py-4">
                  <span
                    className={cn(
                      "flex items-center gap-1.5 text-xs font-medium",
                      tx.status === "Pending" ? "text-amber-500" : "text-primary"
                    )}
                  >
                    <span
                      className={cn(
                        "size-1.5 rounded-full",
                        tx.status === "Pending" ? "bg-amber-500" : "bg-primary"
                      )}
                    />
                    {tx.status}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
