"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { getUserById, type UserSummary } from "@/lib/api";
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

export default function UserDetailsPage() {
  const router = useRouter();
  const params = useParams();

  const userId = params.id as string;

  const [user, setUser] = useState<UserSummary | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = getToken();

    if (!token || !userId) {
      return;
    }

    let cancelled = false;

    getUserById(token, userId)
      .then((result) => {
        if (!cancelled) {
          setUser(result);
        }
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

  if (loading) {
    return (
      <section className="mx-auto max-w-4xl px-6 py-8">
        <p className="text-zinc-500">
          Loading user...
        </p>
      </section>
    );
  }

  if (error) {
    return (
      <section className="mx-auto max-w-4xl px-6 py-8">
        <div className="rounded-lg bg-red-50 px-4 py-3 text-red-700">
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

  if (!user) {
    return null;
  }

  return (
    <section className="mx-auto max-w-4xl px-6 py-8">
      <button
        type="button"
        onClick={() => router.push("/dashboard/users")}
        className="mb-6 text-sm font-medium text-blue-600 hover:text-blue-700"
      >
        ← Back to Users
      </button>

      <div className="rounded-xl bg-white p-6 shadow-sm">
        <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-start">
          <div>
            <h1 className="text-2xl font-bold text-zinc-900">
              User Details
            </h1>

            <p className="mt-1 text-sm text-zinc-500">
              View information about this user.
            </p>
          </div>

          <span
            className={
              user.isActive
                ? "rounded-full bg-green-50 px-3 py-1 text-xs font-medium text-green-700"
                : "rounded-full bg-red-50 px-3 py-1 text-xs font-medium text-red-700"
            }
          >
            {user.isActive ? "Active" : "Inactive"}
          </span>
        </div>

        <div className="mt-8 grid gap-6 sm:grid-cols-2">
          <div>
            <p className="text-sm text-zinc-500">
              Full name
            </p>

            <p className="mt-1 font-medium text-zinc-900">
              {user.fullName}
            </p>
          </div>

          <div>
            <p className="text-sm text-zinc-500">
              Email
            </p>

            <p className="mt-1 font-medium text-zinc-900">
              {user.email}
            </p>
          </div>

          <div>
            <p className="text-sm text-zinc-500">
              Role
            </p>

            <p className="mt-1 font-medium text-zinc-900">
              {getRoleName(user.role)}
            </p>
          </div>

          <div>
            <p className="text-sm text-zinc-500">
              User ID
            </p>

            <p className="mt-1 break-all font-mono text-sm text-zinc-700">
              {user.id}
            </p>
          </div>
        </div>
      </div>
    </section>
  );
}