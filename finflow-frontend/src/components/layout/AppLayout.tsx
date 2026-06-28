import type { ReactNode } from "react";
import AppSidebar from "./AppSidebar";
import AppTopBar from "./AppTopBar";
import AddTransactionModal from "@/components/transactions/AddTransactionModal";

export default function AppLayout({ children }: { children: ReactNode }) {
  return (
    <div className="flex min-h-screen bg-background-dark text-foreground">
      <AppSidebar />
      <div className="flex min-h-screen min-w-0 flex-1 flex-col">
        <AppTopBar />
        <div className="flex-1 overflow-y-auto">{children}</div>
      </div>
      <AddTransactionModal />
    </div>
  );
}
