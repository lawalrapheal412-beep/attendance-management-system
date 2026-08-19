"use client";

import { ReactNode, useEffect } from "react";
import { useRouter } from "next/navigation";
import { getCurrentUser, isAdmin } from "@/lib/auth";

interface DashboardLayoutProps {
  children: ReactNode;
}

export default function DashboardLayout({
  children,
}: DashboardLayoutProps) {
  const router = useRouter();

  useEffect(() => {
    const user = getCurrentUser();

    if (!user || !isAdmin()) {
      router.replace("/login");
    }
  }, [router]);

  const user = getCurrentUser();

  if (!user || !isAdmin()) {
    return (
      <main className="flex min-h-screen items-center justify-center bg-zinc-100">
        <p className="text-sm text-zinc-500">
          Checking authentication...
        </p>
      </main>
    );
  }

  return <>{children}</>;
}