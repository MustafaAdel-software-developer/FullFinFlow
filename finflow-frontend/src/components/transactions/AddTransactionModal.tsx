import { useState, type FormEvent } from "react";
import { useTransactionModal } from "@/context/TransactionModalContext";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Modal } from "@/components/ui/modal";
import { Icon } from "@/components/ui/icon";

export default function AddTransactionModal() {
  const { isOpen, closeModal } = useTransactionModal();
  const [amount, setAmount] = useState("");
  const [description, setDescription] = useState("");

  function handleSubmit(e: FormEvent) {
    e.preventDefault();
    closeModal();
    setAmount("");
    setDescription("");
  }

  return (
    <Modal
      open={isOpen}
      onClose={closeModal}
      title="New Transaction"
      description="Enter the details of your expense or income."
    >
      <form className="space-y-6" onSubmit={handleSubmit}>
        <div className="flex flex-col gap-2">
          <label className="text-xs font-bold uppercase tracking-wider text-text-secondary">
            Amount
          </label>
          <div className="relative">
            <span className="absolute left-4 top-1/2 -translate-y-1/2 text-2xl font-medium text-muted-foreground">
              $
            </span>
            <Input
              value={amount}
              onChange={(e) => setAmount(e.target.value)}
              placeholder="0.00"
              className="border-[#3b5447] bg-[#1c2721] py-4 pl-10 text-3xl font-bold"
            />
          </div>
        </div>

        <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
          <div className="flex flex-col gap-2">
            <label className="text-xs font-bold uppercase tracking-wider text-text-secondary">
              Date
            </label>
            <div className="relative">
              <Input type="date" defaultValue="2023-10-24" className="border-[#3b5447] bg-[#1c2721] pl-11" />
              <Icon
                name="calendar_today"
                className="pointer-events-none absolute left-3.5 top-1/2 -translate-y-1/2 text-muted-foreground"
              />
            </div>
          </div>
          <div className="flex flex-col gap-2">
            <label className="text-xs font-bold uppercase tracking-wider text-text-secondary">
              Description
            </label>
            <Input
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="What was this for?"
              className="border-[#3b5447] bg-[#1c2721]"
            />
          </div>
        </div>

        <div className="flex flex-col gap-2">
          <label className="text-xs font-bold uppercase tracking-wider text-text-secondary">
            Category
          </label>
          <select className="h-11 w-full rounded-lg border border-[#3b5447] bg-[#1c2721] px-4 text-sm text-foreground focus:ring-2 focus:ring-primary/50">
            <option>Food & Dining</option>
            <option>Transportation</option>
            <option>Entertainment</option>
            <option>Income</option>
            <option>Utilities</option>
          </select>
        </div>

        <div className="flex justify-end gap-3 pt-4">
          <Button type="button" variant="outline" onClick={closeModal}>
            Cancel
          </Button>
          <Button type="submit">Save Transaction</Button>
        </div>
      </form>
    </Modal>
  );
}
