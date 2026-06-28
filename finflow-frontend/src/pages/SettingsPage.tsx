import { Icon } from "@/components/ui/icon";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

export default function SettingsPage() {
  return (
    <div className="mx-auto max-w-3xl space-y-8 p-4 md:p-8">
      <div>
        <h2 className="text-3xl font-black tracking-tight">Settings & Security</h2>
        <p className="text-text-secondary">Manage your account preferences and security.</p>
      </div>

      <section className="rounded-xl border border-border-dark bg-card-dark p-6">
        <h3 className="text-xl font-bold">Profile</h3>
        <div className="mt-6 space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="mb-2 block text-sm font-medium">First name</label>
              <Input defaultValue="Alex" className="bg-[#1c2721]" />
            </div>
            <div>
              <label className="mb-2 block text-sm font-medium">Last name</label>
              <Input defaultValue="Rivera" className="bg-[#1c2721]" />
            </div>
          </div>
          <div>
            <label className="mb-2 block text-sm font-medium">Email</label>
            <Input defaultValue="alex@finflow.com" className="bg-[#1c2721]" />
          </div>
          <Button>Save Profile</Button>
        </div>
      </section>

      <section className="rounded-xl border border-border-dark bg-card-dark p-6">
        <h3 className="text-xl font-bold">Account Security</h3>
        <div className="mt-6 space-y-4">
          <div className="flex items-center justify-between rounded-lg border border-border-dark p-4">
            <div className="flex items-center gap-3">
              <Icon name="lock" className="text-primary" />
              <div>
                <p className="font-medium">Password</p>
                <p className="text-sm text-text-secondary">Last changed 30 days ago</p>
              </div>
            </div>
            <Button variant="outline" size="sm">Change</Button>
          </div>
          <div className="flex items-center justify-between rounded-lg border border-border-dark p-4">
            <div className="flex items-center gap-3">
              <Icon name="verified_user" className="text-primary" />
              <div>
                <p className="font-medium">Two-factor authentication</p>
                <p className="text-sm text-text-secondary">Not enabled</p>
              </div>
            </div>
            <Button variant="outline" size="sm">Enable</Button>
          </div>
        </div>
      </section>

      <section className="rounded-xl border border-border-dark bg-card-dark p-6">
        <h3 className="text-xl font-bold">Preferences</h3>
        <div className="mt-6 space-y-4">
          <div className="flex items-center justify-between">
            <span className="text-sm">Email notifications</span>
            <input type="checkbox" defaultChecked className="accent-primary" />
          </div>
          <div className="flex items-center justify-between">
            <span className="text-sm">Budget alerts</span>
            <input type="checkbox" defaultChecked className="accent-primary" />
          </div>
          <div className="flex items-center justify-between">
            <span className="text-sm">Weekly summary</span>
            <input type="checkbox" className="accent-primary" />
          </div>
        </div>
      </section>
    </div>
  );
}
