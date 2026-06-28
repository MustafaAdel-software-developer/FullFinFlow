import { mockNotifications } from "@/data/mock/notifications";
import { Icon } from "@/components/ui/icon";
import { cn } from "@/lib/utils";

export default function NotificationsPage() {
  return (
    <div className="space-y-8 p-4 md:p-8">
      <div>
        <h2 className="text-3xl font-black tracking-tight text-white">Notifications</h2>
        <p className="text-text-secondary">Stay updated on your financial activity.</p>
      </div>

      <div className="space-y-3">
        {mockNotifications.map((n) => (
          <div
            key={n.id}
            className={cn(
              "flex gap-4 rounded-xl border border-border-dark p-4 transition-colors",
              n.read ? "bg-card-dark/50" : "bg-card-dark"
            )}
          >
            <div
              className={cn(
                "flex size-10 shrink-0 items-center justify-center rounded-lg",
                n.type === "warning" && "bg-amber-500/10 text-amber-500",
                n.type === "success" && "bg-primary/10 text-primary",
                n.type === "info" && "bg-blue-500/10 text-blue-400"
              )}
            >
              <Icon name={n.icon} />
            </div>
            <div className="flex-1">
              <div className="flex items-start justify-between gap-2">
                <h4 className="font-semibold">{n.title}</h4>
                {!n.read && <span className="size-2 shrink-0 rounded-full bg-primary" />}
              </div>
              <p className="mt-1 text-sm text-text-secondary">{n.message}</p>
              <p className="mt-2 text-xs text-muted-foreground">{n.time}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
