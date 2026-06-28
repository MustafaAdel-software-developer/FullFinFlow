import { Link, NavLink } from "react-router-dom";
import { Icon } from "@/components/ui/icon";
import { cn } from "@/lib/utils";

const mainNav = [
  { label: "Dashboard", href: "/", icon: "dashboard" },
  { label: "Accounts", href: "/accounts", icon: "account_balance" },
  { label: "Transactions", href: "/transactions", icon: "receipt_long" },
  { label: "Budgets", href: "/budgets", icon: "savings" },
  { label: "Goals", href: "/goals", icon: "track_changes" },
  { label: "Reports", href: "/reports", icon: "bar_chart" },
];

const secondaryNav = [
  { label: "Insights", href: "/insights", icon: "insights" },
  { label: "Investments", href: "/investments", icon: "trending_up" },
  { label: "Settings", href: "/settings", icon: "settings" },
];

export default function AppSidebar() {
  return (
    <aside className="hidden w-64 shrink-0 flex-col border-r border-border-dark bg-background-dark md:flex sticky top-0 h-screen">
      <div className="flex items-center gap-3 p-6">
        <div className="flex size-8 items-center justify-center rounded-lg bg-primary text-primary-foreground">
          <Icon name="account_balance_wallet" className="text-xl" />
        </div>
        <h1 className="text-xl font-bold tracking-tight">FinFlow</h1>
      </div>

      <nav className="mt-4 flex-1 space-y-1 px-4">
        {mainNav.map((item) => (
          <NavLink
            key={item.href}
            to={item.href}
            end={item.href === "/"}
            className={({ isActive }) =>
              cn(
                "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                isActive
                  ? "active-nav"
                  : "text-muted-foreground hover:bg-card-dark hover:text-foreground"
              )
            }
          >
            <Icon name={item.icon} />
            {item.label}
          </NavLink>
        ))}

        <div className="my-4 border-t border-border-dark" />

        {secondaryNav.map((item) => (
          <NavLink
            key={item.href}
            to={item.href}
            className={({ isActive }) =>
              cn(
                "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                isActive
                  ? "active-nav"
                  : "text-muted-foreground hover:bg-card-dark hover:text-foreground"
              )
            }
          >
            <Icon name={item.icon} />
            {item.label}
          </NavLink>
        ))}
      </nav>

      <div className="border-t border-border-dark p-4">
        <div className="flex items-center gap-3 p-2">
          <div
            className="size-9 rounded-full bg-muted bg-cover bg-center"
            style={{
              backgroundImage:
                "url('https://lh3.googleusercontent.com/aida-public/AB6AXuA-msPB42BEwhnBHtOCqXvFIQJbfnxjcooNaAUVECBODXym0hAvLWpUZkwdzjU76CRoULzGKHlE5hRgjBaQtb_zE5NKTNyiToT6MaWz8K9eYqgDaKAPGec9p19FmeSvmThoShgwS1WS16vxS0VcM-o0YyU8WZNAlWOh7i-ry_q1w0EoeA3adwaOtDbd3lokFQlA4OoN8FxDsKtbXEDi9MsqKPDajxYOIZhW92P5xfrfyHf3vrQbpHVoqmFFwQxMiiMAdCRhg2E4aPnX')",
            }}
          />
          <div className="flex flex-col">
            <span className="text-sm font-medium">Alex Rivera</span>
            <span className="text-xs text-text-secondary">Pro Plan</span>
          </div>
        </div>
        <Link
          to="/login"
          className="mt-3 block text-center text-xs text-text-secondary hover:text-primary"
        >
          Sign in / Register
        </Link>
      </div>
    </aside>
  );
}
