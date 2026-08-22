"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";

import {
  getUserById,
  updateUser,
  type UserSummary,
} from "@/lib/api";

import { getToken } from "@/lib/auth";

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

export default function EditUserPage() {
  const router = useRouter();
  const params = useParams();

  const userId = params.id as string;

  const [user, setUser] = useState<UserSummary | null>(null);

  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [role, setRole] = useState(2);

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    const token = getToken();

    if (!token || !userId) {
      return;
    }

    let cancelled = false;

    getUserById(token, userId)
      .then((result) => {
        if (cancelled) {
          return;
        }

        setUser(result);
        setFullName(result.fullName);
        setEmail(result.email);
        setRole(result.role);
      })
      .catch((err) => {
        if (!cancelled) {
          setError(
            err instanceof Error
              ? err.message
              : "Unable to load user.",
          );
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
  }, [userId]);

  async function handleSubmit(
    event: React.FormEvent<HTMLFormElement>,
  ) {
    event.preventDefault();

    setError("");
    setSuccess("");

    const token = getToken();

    if (!token) {
      setError("Your session has expired.");
      return;
    }

    if (!fullName.trim()) {
      setError("Full name is required.");
      return;
    }

    if (!email.trim()) {
      setError("Email is required.");
      return;
    }

    setSaving(true);

    try {
      const updatedUser = await updateUser(token, userId, {
        id: userId,
        fullName: fullName.trim(),
        email: email.trim(),
        role,
        isActive: user?.isActive ?? true,
      });

      setUser(updatedUser);

      setSuccess("User updated successfully.");

      setTimeout(() => {
        router.push(`/dashboard/users/${userId}`);
      }, 1000);
    } catch (err) {
      setError(
        err instanceof Error
          ? err.message
          : "Unable to update user.",
      );
    } finally {
      setSaving(false);
    }
  }

  if (loading) {
    return (
      <section className="mx-auto max-w-4xl px-6 py-8">
        <p className="text-zinc-500">
          Loading user...
        </p>
      </section>
    );
  }

  if (error && !user) {
    return (
      <section className="mx-auto max-w-4xl px-6 py-8">
        <div className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
          {error}
        </div>

        <button
          type="button"
          onClick={() => router.push("/dashboard/users")}
          className="mt-4 rounded-lg border border-zinc-300 px-4 py-2 text-sm font-medium hover:bg-zinc-50"
        >
          Back to Users
        </button>
      </section>
    );
  }

  return (
    <section className="mx-auto max-w-4xl px-6 py-8">
      <button
        type="button"
        onClick={() =>
          router.push(`/dashboard/users/${userId}`)
        }
        className="mb-6 text-sm font-medium text-blue-600 hover:text-blue-700"
      >
        ← Back to User Details
      </button>

      <div className="rounded-xl bg-white p-6 shadow-sm">
        <div className="mb-8">
          <h1 className="text-2xl font-bold text-zinc-900">
            Edit User
          </h1>

          <p className="mt-1 text-sm text-zinc-500">
            Update this user information.
          </p>
        </div>

        <form
          onSubmit={handleSubmit}
          className="space-y-6"
        >
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
              className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
            />
          </div>

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
              className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
            />
          </div>

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

          {user && (
            <div className="rounded-lg bg-zinc-50 px-4 py-3 text-sm text-zinc-600">
              Current role:{" "}
              <span className="font-medium text-zinc-900">
                {getRoleName(user.role)}
              </span>
            </div>
          )}

          {error && (
            <div className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
              {error}
            </div>
          )}

          {success && (
            <div className="rounded-lg bg-green-50 px-4 py-3 text-sm text-green-700">
              {success}
            </div>
          )}

          <div className="flex gap-3">
            <button
              type="submit"
              disabled={saving}
              className="rounded-lg bg-blue-600 px-5 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {saving ? "Saving..." : "Save Changes"}
            </button>

            <button
              type="button"
              onClick={() =>
                router.push(`/dashboard/users/${userId}`)
              }
              disabled={saving}
              className="rounded-lg border border-zinc-300 px-5 py-3 font-semibold text-zinc-700 transition hover:bg-zinc-50 disabled:cursor-not-allowed disabled:opacity-60"
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </section>
  );
}