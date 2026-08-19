import { Suspense } from "react";
import SetPasswordForm from "./SetPasswordForm";

export default function SetupPasswordPage() {
  return (
    <Suspense
      fallback={
        <main className="flex min-h-screen items-center justify-center bg-zinc-100 px-6">
          <div className="text-sm text-zinc-500">
            Loading password setup...
          </div>
        </main>
      }
    >
      <SetPasswordForm />
    </Suspense>
  );
}