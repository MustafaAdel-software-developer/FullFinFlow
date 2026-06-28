import { Link } from "react-router-dom";
import { useTransactionModal } from "@/context/TransactionModalContext";
import { Button } from "@/components/ui/button";
import { Icon } from "@/components/ui/icon";
import { Input } from "@/components/ui/input";

export default function AppTopBar() {
  const { openModal } = useTransactionModal();

  return (
    <header className="sticky top-0 z-10 flex h-16 shrink-0 items-center gap-4 border-b border-border-dark bg-background-dark px-6 md:px-8">
      <div className="relative min-w-0 flex-1 max-w-md">
        <Icon
          name="search"
          className="pointer-events-none absolute left-3 top-1/2 z-10 -translate-y-1/2 text-[1.125rem] text-muted-foreground"
        />
        <Input
          placeholder="Search transactions, bills, or goals..."
          className="h-10 w-full border-none bg-card-dark py-2 pl-10 pr-4 text-sm focus-visible:ring-1"
        />
      </div>

      <div className="flex shrink-0 items-center gap-2">
        <Link
          to="/notifications"
          className="flex size-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:bg-card-dark hover:text-foreground"
        >
          <Icon name="notifications" className="text-[1.25rem]" />
        </Link>
        <Link
          to="/settings"
          className="flex size-9 items-center justify-center rounded-lg text-muted-foreground transition-colors hover:bg-card-dark hover:text-foreground"
        >
          <Icon name="settings" className="text-[1.25rem]" />
        </Link>
        <Button
          onClick={openModal}
          size="sm"
          className="ml-1 hidden h-9 px-4 text-sm font-semibold sm:inline-flex"
        >
          + New Transaction
        </Button>
        <Button onClick={openModal} size="icon" className="size-9 sm:hidden">
          <Icon name="add" />
        </Button>
      </div>
    </header>
  );
}
