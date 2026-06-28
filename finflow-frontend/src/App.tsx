import { BrowserRouter, Route, Routes } from "react-router-dom";
import { TransactionModalProvider } from "@/context/TransactionModalContext";
import AppLayout from "@/components/layout/AppLayout";
import DashboardPage from "@/pages/DashboardPage";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
import AccountsPage from "@/pages/AccountsPage";
import TransactionsPage from "@/pages/TransactionsPage";
import BudgetsPage from "@/pages/BudgetsPage";
import GoalsPage from "@/pages/GoalsPage";
import ReportsPage from "@/pages/ReportsPage";
import InsightsPage from "@/pages/InsightsPage";
import InvestmentsPage from "@/pages/InvestmentsPage";
import NotificationsPage from "@/pages/NotificationsPage";
import SettingsPage from "@/pages/SettingsPage";

function App() {
  return (
    <TransactionModalProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/" element={<AppLayout><DashboardPage /></AppLayout>} />
          <Route path="/accounts" element={<AppLayout><AccountsPage /></AppLayout>} />
          <Route path="/transactions" element={<AppLayout><TransactionsPage /></AppLayout>} />
          <Route path="/budgets" element={<AppLayout><BudgetsPage /></AppLayout>} />
          <Route path="/goals" element={<AppLayout><GoalsPage /></AppLayout>} />
          <Route path="/reports" element={<AppLayout><ReportsPage /></AppLayout>} />
          <Route path="/insights" element={<AppLayout><InsightsPage /></AppLayout>} />
          <Route path="/investments" element={<AppLayout><InvestmentsPage /></AppLayout>} />
          <Route path="/notifications" element={<AppLayout><NotificationsPage /></AppLayout>} />
          <Route path="/settings" element={<AppLayout><SettingsPage /></AppLayout>} />
        </Routes>
      </BrowserRouter>
    </TransactionModalProvider>
  );
}

export default App;
