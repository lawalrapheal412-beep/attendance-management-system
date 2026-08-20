"use client";

import { useEffect, useState } from "react";
import { getUsers } from "@/lib/api";
import { getToken } from "@/lib/auth";

interface UserSummary {
  id: string;
  fullName: string;
  email: string;
  role: number;
  isActive: boolean;
}

function getRoleName(role: number): string {
  switch (role) {
    case 0:
      return "Admin";
    case 1:
      return "Lecturer";
    case 2:
      return "Student";
    default:
      return "Unknown";
  }
}

export default function UsersPage() {
  const [users, setUsers] = useState<UserSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
  const token = getToken();

  if (!token) {
    return;
  }

  let cancelled = false;

  getUsers(token)
    .then((result) => {
      if (!cancelled) {
        setUsers(result as UserSummary[]);
      }
    })
    .catch(() => {
      if (!cancelled) {
        setError("Unable to load users.");
      }
    })
    .finally(() => {
      if (!cancelled) {
        setLoading(false);
      }
    });

  return () => {
    cancelled = true;
  };
}, []);

  return (
    <section className="mx-auto max-w-7xl px-6 py-8">
      <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-center">
        <div>
          <h1 className="text-2xl font-bold text-zinc-900">
            Users
          </h1>

          <p className="mt-1 text-sm text-zinc-500">
            Manage users in the attendance management system.
          </p>
        </div>

        <button
          type="button"
          className="rounded-lg bg-blue-600 px-4 py-2.5 text-sm font-semibold text-white transition hover:bg-blue-700"
        >
          + Add User
        </button>
      </div>

      {error && (
        <div className="mt-6 rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
          {error}
        </div>
      )}

      <div className="mt-8 overflow-hidden rounded-xl bg-white shadow-sm">
        <div className="overflow-x-auto">
          <table className="w-full text-left text-sm">
            <thead>
              <tr className="border-b bg-zinc-50 text-zinc-500">
                <th className="px-6 py-4 font-medium">
                  Name
                </th>

                <th className="px-6 py-4 font-medium">
                  Email
                </th>

                <th className="px-6 py-4 font-medium">
                  Role
                </th>

                <th className="px-6 py-4 font-medium">
                  Status
                </th>
              </tr>
            </thead>

            <tbody>
              {users.map((user) => (
                <tr
                  key={user.id}
                  className="border-b last:border-0 hover:bg-zinc-50"
                >
                  <td className="px-6 py-4 font-medium text-zinc-900">
                    {user.fullName}
                  </td>

                  <td className="px-6 py-4 text-zinc-600">
                    {user.email}
                  </td>

                  <td className="px-6 py-4 text-zinc-600">
                    {getRoleName(user.role)}
                  </td>

                  <td className="px-6 py-4">
                    <span
                      className={
                        user.isActive
                          ? "rounded-full bg-green-50 px-3 py-1 text-xs font-medium text-green-700"
                          : "rounded-full bg-red-50 px-3 py-1 text-xs font-medium text-red-700"
                      }
                    >
                      {user.isActive ? "Active" : "Inactive"}
                    </span>
                  </td>
                </tr>
              ))}

              {loading && (
                <tr>
                  <td
                    colSpan={4}
                    className="px-6 py-10 text-center text-zinc-500"
                  >
                    Loading users...
                  </td>
                </tr>
              )}

              {!loading && !error && users.length === 0 && (
                <tr>
                  <td
                    colSpan={4}
                    className="px-6 py-10 text-center text-zinc-500"
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
  );
}