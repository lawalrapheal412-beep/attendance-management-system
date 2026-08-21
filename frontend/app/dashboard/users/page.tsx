"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

import { getToken } from "@/lib/auth";

import {
  createUser,
  getUsers,
  type UserSummary,
} from "@/lib/api";

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
  const router = useRouter();

  const [users, setUsers] = useState<UserSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const [showCreateForm, setShowCreateForm] = useState(false);

  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [role, setRole] = useState(2);

  const [creating, setCreating] = useState(false);
  const [createError, setCreateError] = useState("");
  const [createSuccess, setCreateSuccess] = useState("");

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

  async function handleCreateUser(
    event: React.FormEvent<HTMLFormElement>,
  ) {
    event.preventDefault();

    setCreateError("");
    setCreateSuccess("");

    const token = getToken();

    if (!token) {
      setCreateError("Your session has expired.");
      return;
    }

    setCreating(true);

    try {
      await createUser(token, {
        fullName: fullName.trim(),
        email: email.trim(),
        role,
      });

      setCreateSuccess(
        "User created successfully. A password setup process can now be initiated for the user.",
      );

      setFullName("");
      setEmail("");
      setRole(2);

      const updatedUsers = await getUsers(token);
      setUsers(updatedUsers as UserSummary[]);
    } catch (err) {
      setCreateError(
        err instanceof Error
          ? err.message
          : "Unable to create user.",
      );
    } finally {
      setCreating(false);
    }
  }

  function handleToggleCreateForm() {
    setShowCreateForm((current) => !current);
    setCreateError("");
    setCreateSuccess("");
  }

  return (
    <section className="mx-auto max-w-7xl px-6 py-8">

      {/* Page Header */}
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
          onClick={handleToggleCreateForm}
          className="rounded-lg bg-blue-600 px-4 py-2.5 text-sm font-semibold text-white transition hover:bg-blue-700"
        >
          {showCreateForm ? "Cancel" : "+ Add User"}
        </button>
      </div>

      {/* Create User Form */}
      {showCreateForm && (
        <div className="mt-8 rounded-xl bg-white p-6 shadow-sm">
          <h2 className="text-lg font-semibold text-zinc-900">
            Create User
          </h2>

          <p className="mt-1 text-sm text-zinc-500">
            Create a new administrator, lecturer or student.
          </p>

          <form
            onSubmit={handleCreateUser}
            className="mt-6 space-y-5"
          >
            {/* Full Name */}
            <div>
              <label
                htmlFor="fullName"
                className="mb-2 block text-sm font-medium text-zinc-700"
              >
                Full name
              </label>

              <input
                id="fullName"
                type="text"
                value={fullName}
                onChange={(event) =>
                  setFullName(event.target.value)
                }
                required
                placeholder="Enter full name"
                className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
              />
            </div>

            {/* Email */}
            <div>
              <label
                htmlFor="email"
                className="mb-2 block text-sm font-medium text-zinc-700"
              >
                Email
              </label>

              <input
                id="email"
                type="email"
                value={email}
                onChange={(event) =>
                  setEmail(event.target.value)
                }
                required
                placeholder="Enter email address"
                className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
              />
            </div>

            {/* Role */}
            <div>
              <label
                htmlFor="role"
                className="mb-2 block text-sm font-medium text-zinc-700"
              >
                Role
              </label>

              <select
                id="role"
                value={role}
                onChange={(event) =>
                  setRole(Number(event.target.value))
                }
                className="w-full rounded-lg border border-zinc-300 bg-white px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
              >
                <option value={0}>Admin</option>
                <option value={1}>Lecturer</option>
                <option value={2}>Student</option>
              </select>
            </div>

            {/* Error */}
            {createError && (
              <div className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
                {createError}
              </div>
            )}

            {/* Success */}
            {createSuccess && (
              <div className="rounded-lg bg-green-50 px-4 py-3 text-sm text-green-700">
                {createSuccess}
              </div>
            )}

            {/* Submit */}
            <button
              type="submit"
              disabled={creating}
              className="rounded-lg bg-blue-600 px-5 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {creating ? "Creating..." : "Create User"}
            </button>
          </form>
        </div>
      )}

      {/* General Error */}
      {error && (
        <div className="mt-6 rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
          {error}
        </div>
      )}

      {/* Users Table */}
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

                <th className="px-6 py-4 font-medium">
                  Actions
                </th>
              </tr>
            </thead>

            <tbody>
              {users.map((user) => (
                <tr
                  key={user.id}
                  className="border-b last:border-0 hover:bg-zinc-50"
                >
                  {/* Name */}
                  <td className="px-6 py-4 font-medium text-zinc-900">
                    {user.fullName}
                  </td>

                  {/* Email */}
                  <td className="px-6 py-4 text-zinc-600">
                    {user.email}
                  </td>

                  {/* Role */}
                  <td className="px-6 py-4">
                    {getRoleName(user.role)}
                  </td>

                  {/* Status */}
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

                  {/* Actions */}
                  <td className="px-6 py-4">
                    <button
                      type="button"
                      onClick={() =>
                        router.push(
                          `/dashboard/users/${user.id}`,
                        )
                      }
                      className="font-medium text-blue-600 hover:text-blue-700"
                    >
                      View
                    </button>
                  </td>
                </tr>
              ))}

              {/* Loading */}
              {loading && (
                <tr>
                  <td
                    colSpan={5}
                    className="px-6 py-10 text-center text-zinc-500"
                  >
                    Loading users...
                  </td>
                </tr>
              )}

              {/* Empty */}
              {!loading &&
                !error &&
                users.length === 0 && (
                  <tr>
                    <td
                      colSpan={5}
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