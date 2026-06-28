import { createContext, useContext, useState, type ReactNode } from "react";

type TransactionModalContextValue = {
  isOpen: boolean;
  openModal: () => void;
  closeModal: () => void;
};

const TransactionModalContext = createContext<TransactionModalContextValue | null>(null);

export function TransactionModalProvider({ children }: { children: ReactNode }) {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <TransactionModalContext.Provider
      value={{
        isOpen,
        openModal: () => setIsOpen(true),
        closeModal: () => setIsOpen(false),
      }}
    >
      {children}
    </TransactionModalContext.Provider>
  );
}

export function useTransactionModal() {
  const ctx = useContext(TransactionModalContext);
  if (!ctx) throw new Error("useTransactionModal must be used within TransactionModalProvider");
  return ctx;
}
