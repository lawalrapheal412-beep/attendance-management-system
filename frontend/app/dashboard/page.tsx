"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getUsers } from "@/lib/api";

interface UserSummary {
  id: string;
  fullName: string;
  email: string;
  role: number;
  isActive: boolean;
}

export default function DashboardPage() {
  const router = useRouter();

  const [user, setUser] = useState<{
    fullName: string;
    email: string;
  } | null>(null);

  const [users, setUsers] = useState<UserSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = sessionStorage.getItem("ams_token");
    const storedUser = sessionStorage.getItem("ams_user");

    if (!token || !storedUser) {
      router.replace("/login");
      return;
    }

    setUser(JSON.parse(storedUser));

    getUsers(token)
      .then((result) => {
        setUsers(result as UserSummary[]);
      })
      .catch(() => {
        sessionStorage.removeItem("ams_token");
        sessionStorage.removeItem("ams_user");
        setError("Your session has expired.");
        router.replace("/login");
      })
      .finally(() => {
        setLoading(false);
      });
  }, [router]);

  function logout() {
    sessionStorage.removeItem("ams_token");
    sessionStorage.removeItem("ams_user");

    router.replace("/login");
  }

  return (
    <main className="min-h-screen bg-zinc-100">
      <header className="border-b bg-white">
        <div className="mx-auto flex max-w-7xl items-center justify-between px-6 py-4">
          <div>
            <h1 className="text-xl font-bold text-zinc-900">
              Attendance Management System
            </h1>
            <p className="text-sm text-zinc-500">
              Admin Dashboard
            </p>
          </div>

          <div className="flex items-center gap-4">
            {user && (
              <div className="text-right">
                <p className="text-sm font-semibold text-zinc-900">
                  {user.fullName}
                </p>
                <p className="text-xs text-zinc-500">
                  {user.email}
                </p>
              </div>
            )}

            <button
              onClick={logout}
              className="rounded-lg border border-zinc-300 px-4 py-2 text-sm font-medium hover:bg-zinc-50"
            >
              Logout
            </button>
          </div>
        </div>
      </header>

      <section className="mx-auto max-w-7xl px-6 py-8">
        <h2 className="text-2xl font-bold text-zinc-900">
          Dashboard
        </h2>

        <p className="mt-1 text-zinc-500">
          Manage your attendance system from one place.
        </p>

        {error && (
          <div className="mt-6 rounded-lg bg-red-50 px-4 py-3 text-red-700">
            {error}
          </div>
        )}

        <div className="mt-8 grid gap-6 md:grid-cols-3">
          <div className="rounded-xl bg-white p-6 shadow-sm">
            <p className="text-sm text-zinc-500">Total users</p>
            <p className="mt-2 text-3xl font-bold text-zinc-900">
              {loading ? "..." : users.length}
            </p>
          </div>

          <div className="rounded-xl bg-white p-6 shadow-sm">
            <p className="text-sm text-zinc-500">Students</p>
            <p className="mt-2 text-3xl font-bold text-zinc-900">—</p>
          </div>

          <div className="rounded-xl bg-white p-6 shadow-sm">
            <p className="text-sm text-zinc-500">Attendance sessions</p>
            <p className="mt-2 text-3xl font-bold text-zinc-900">—</p>
          </div>
        </div>

        <div className="mt-8 rounded-xl bg-white p-6 shadow-sm">
          <h3 className="text-lg font-semibold text-zinc-900">
            Users
          </h3>

          <div className="mt-4 overflow-x-auto">
            <table className="w-full text-left text-sm">
              <thead>
                <tr className="border-b text-zinc-500">
                  <th className="px-3 py-3">Name</th>
                  <th className="px-3 py-3">Email</th>
                  <th className="px-3 py-3">Role</th>
                  <th className="px-3 py-3">Status</th>
                </tr>
              </thead>

              <tbody>
                {users.map((item) => (
                  <tr
                    key={item.id}
                    className="border-b last:border-0"
                  >
                    <td className="px-3 py-3 font-medium">
                      {item.fullName}
                    </td>

                    <td className="px-3 py-3 text-zinc-600">
                      {item.email}
                    </td>

                    <td className="px-3 py-3">
                      {item.role === 0
                        ? "Admin"
                        : item.role === 1
                          ? "Lecturer"
                          : "Student"}
                    </td>

                    <td className="px-3 py-3">
                      {item.isActive ? "Active" : "Inactive"}
                    </td>
                  </tr>
                ))}

                {!loading && users.length === 0 && (
                  <tr>
                    <td
                      colSpan={4}
                      className="px-3 py-8 text-center text-zinc-500"
                    >
                      No users found.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </section>
    </main>
  );
}