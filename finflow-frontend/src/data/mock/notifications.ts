export type Notification = {
  id: string;
  title: string;
  message: string;
  time: string;
  read: boolean;
  icon: string;
  type: "info" | "warning" | "success";
};

export const mockNotifications: Notification[] = [
  { id: "1", title: "Budget alert", message: "Food & Dining is at 70% of your monthly limit.", time: "2 hours ago", read: false, icon: "warning", type: "warning" },
  { id: "2", title: "Transaction completed", message: "Your payment to Apple Store was processed.", time: "5 hours ago", read: false, icon: "check_circle", type: "success" },
  { id: "3", title: "Goal milestone", message: "Emergency Fund reached 65% of target!", time: "1 day ago", read: true, icon: "emoji_events", type: "success" },
  { id: "4", title: "Account synced", message: "Chase Primary Checking synced successfully.", time: "2 days ago", read: true, icon: "sync", type: "info" },
];
