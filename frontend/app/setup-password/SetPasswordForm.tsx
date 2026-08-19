"use client";

import { FormEvent, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";

export default function SetPasswordPage() {
  const router = useRouter();
  const searchParams = useSearchParams();

  const token = searchParams.get("token");

  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [loading, setLoading] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError("");
    setSuccess("");

    if (!token) {
      setError("This password setup link is invalid.");
      return;
    }

    if (password.length < 8) {
      setError("Password must be at least 8 characters.");
      return;
    }

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    setLoading(true);

    try {
      const response = await fetch("/api/backend/auth/set-password", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          token,
          password,
        }),
      });

      if (!response.ok) {
        if (response.status === 400) {
          const message = await response.text();

          setError(
            message
              ? message.replace(/^"|"$/g, "")
              : "This password setup link is invalid or expired.",
          );

          return;
        }

        setError("Unable to set your password. Please try again.");
        return;
      }

      setSuccess(
        "Your password has been created successfully. Redirecting to login...",
      );

      setTimeout(() => {
        router.replace("/login");
      }, 1500);
    } catch {
      setError(
        "Unable to connect to the server. Please try again.",
      );
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-zinc-100 px-6">
      <div className="w-full max-w-md rounded-2xl bg-white p-8 shadow-lg">
        <div className="mb-8">
          <p className="text-sm font-medium text-blue-600">
            Attendance Management System
          </p>

          <h1 className="mt-2 text-3xl font-bold text-zinc-900">
            Create your password
          </h1>

          <p className="mt-2 text-sm text-zinc-500">
            Choose a secure password for your account.
          </p>
        </div>

        {!token && (
          <div className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">
            This password setup link is invalid or missing its token.
          </div>
        )}

        {token && (
          <form
            onSubmit={handleSubmit}
            className="space-y-5"
          >
            <div>
              <label
                htmlFor="password"
                className="mb-2 block text-sm font-medium text-zinc-700"
              >
                New password
              </label>

              <input
                id="password"
                type="password"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                required
                minLength={8}
                autoComplete="new-password"
                className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
                placeholder="At least 8 characters"
              />
            </div>

            <div>
              <label
                htmlFor="confirmPassword"
                className="mb-2 block text-sm font-medium text-zinc-700"
              >
                Confirm password
              </label>

              <input
                id="confirmPassword"
                type="password"
                value={confirmPassword}
                onChange={(event) =>
                  setConfirmPassword(event.target.value)
                }
                required
                minLength={8}
                autoComplete="new-password"
                className="w-full rounded-lg border border-zinc-300 px-4 py-3 outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
                placeholder="Enter the password again"
              />
            </div>

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

            <button
              type="submit"
              disabled={loading}
              className="w-full rounded-lg bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {loading ? "Setting password..." : "Set password"}
            </button>
          </form>
        )}
      </div>
    </main>
  );
}