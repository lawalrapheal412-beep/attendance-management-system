"use client";

import { useRouter } from "next/navigation";
import { getCurrentUser, logout } from "@/lib/auth";

export default function Header() {
  const router = useRouter();
  const user = getCurrentUser();

  function handleLogout() {
    logout();
    router.replace("/login");
  }

  return (
    <header className="border-b bg-white">
      <div className="flex min-h-16 items-center justify-between px-6">
        <div>
          <h2 className="text-lg font-semibold text-zinc-900">
            Admin Dashboard
          </h2>

          <p className="text-xs text-zinc-500">
            Manage your attendance system
          </p>
        </div>

        <div className="flex items-center gap-4">
          {user && (
            <div className="hidden text-right sm:block">
              <p className="text-sm font-semibold text-zinc-900">
                {user.fullName}
              </p>

              <p className="text-xs text-zinc-500">
                {user.email}
              </p>
            </div>
          )}

          <button
            type="button"
            onClick={handleLogout}
            className="rounded-lg border border-zinc-300 px-4 py-2 text-sm font-medium text-zinc-700 transition hover:bg-zinc-50"
          >
            Logout
          </button>
        </div>
      </div>
    </header>
  );
}