import { Link, useNavigate } from "react-router-dom";
import { useState, type FormEvent } from "react";
import { Icon } from "@/components/ui/icon";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { api, saveAuthToken } from "@/lib/api";

export default function RegisterPage() {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
    country: "Egypt",
    city: "",
    zipCode: "",
    addressLine: "",
  });
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const res = await api.register(form);
      saveAuthToken(res.token);
      navigate("/");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Registration failed");
    } finally {
      setLoading(false);
    }
  }

  function update(field: keyof typeof form, value: string) {
    setForm((c) => ({ ...c, [field]: value }));
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
              <h2 className="mt-6 text-2xl font-semibold">Create account</h2>
              <p className="mt-2 text-center text-base text-text-secondary">
                Start managing your finances today.
              </p>
            </div>

            <form className="space-y-4" onSubmit={handleSubmit}>
              <div className="grid grid-cols-2 gap-4">
                <div className="flex flex-col gap-2">
                  <label className="text-sm font-medium">First name</label>
                  <Input required value={form.firstName} onChange={(e) => update("firstName", e.target.value)} className="border-[#3b5447] bg-[#1c2721]" />
                </div>
                <div className="flex flex-col gap-2">
                  <label className="text-sm font-medium">Last name</label>
                  <Input required value={form.lastName} onChange={(e) => update("lastName", e.target.value)} className="border-[#3b5447] bg-[#1c2721]" />
                </div>
              </div>
              <div className="flex flex-col gap-2">
                <label className="text-sm font-medium">Email</label>
                <Input type="email" required value={form.email} onChange={(e) => update("email", e.target.value)} className="border-[#3b5447] bg-[#1c2721]" />
              </div>
              <div className="grid grid-cols-2 gap-4">
                <div className="flex flex-col gap-2">
                  <label className="text-sm font-medium">Password</label>
                  <Input type="password" required value={form.password} onChange={(e) => update("password", e.target.value)} className="border-[#3b5447] bg-[#1c2721]" />
                </div>
                <div className="flex flex-col gap-2">
                  <label className="text-sm font-medium">Confirm</label>
                  <Input type="password" required value={form.confirmPassword} onChange={(e) => update("confirmPassword", e.target.value)} className="border-[#3b5447] bg-[#1c2721]" />
                </div>
              </div>
              {error && <p className="text-sm text-destructive">{error}</p>}
              <Button type="submit" className="h-12 w-full" disabled={loading}>
                {loading ? "Creating..." : "Create Account"}
              </Button>
            </form>

            <p className="mt-6 text-center text-sm text-text-secondary">
              Already have an account?{" "}
              <Link to="/login" className="font-medium text-primary hover:underline">
                Sign in
              </Link>
            </p>
          </div>
        </div>
      </main>
    </div>
  );
}
