import { Link, useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import { Icon } from "@/components/ui/icon";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { api, saveAuthToken } from "@/lib/api";

export default function LoginPage() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const res = await api.login(email, password);
      saveAuthToken(res.token);
      navigate("/");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Login failed");
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="auth-gradient relative flex min-h-screen items-center justify-center p-6">
      <main className="relative z-10 w-full max-w-[440px]">
        <div className="overflow-hidden rounded-xl border border-[#3b5447] bg-[#1c2721] shadow-2xl">
          <div className="p-8 sm:p-10">
            <div className="mb-8 flex flex-col items-center">
              <div className="mb-4 flex h-12 w-12 items-center justify-center rounded-lg bg-primary shadow-lg shadow-primary/20">
                <Icon name="account_balance_wallet" className="text-3xl text-primary-foreground" />
              </div>
              <h1 className="text-[32px] font-bold tracking-tight">FinFlow</h1>
              <h2 className="mt-6 text-2xl font-semibold">Welcome back</h2>
              <p className="mt-2 text-center text-base text-text-secondary">
                Please enter your details to access your account.
              </p>
            </div>

            <form className="space-y-5" onSubmit={handleSubmit}>
              <div className="flex flex-col gap-2">
                <label className="text-sm font-medium">Email</label>
                <Input
                  type="email"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  placeholder="name@company.com"
                  className="h-12 border-[#3b5447] bg-[#1c2721]"
                />
              </div>
              <div className="flex flex-col gap-2">
                <label className="text-sm font-medium">Password</label>
                <Input
                  type="password"
                  required
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="••••••••"
                  className="h-12 border-[#3b5447] bg-[#1c2721]"
                />
              </div>
              {error && <p className="text-sm text-destructive">{error}</p>}
              <Button type="submit" className="h-12 w-full text-base" disabled={loading}>
                {loading ? "Signing in..." : "Sign In"}
              </Button>
            </form>

            <p className="mt-6 text-center text-sm text-text-secondary">
              Don&apos;t have an account?{" "}
              <Link to="/register" className="font-medium text-primary hover:underline">
                Sign up
              </Link>
            </p>
          </div>
        </div>
      </main>
    </div>
  );
}
