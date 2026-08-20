"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";

const navigation = [
  {
    name: "Dashboard",
    href: "/dashboard",
  },
  {
    name: "Users",
    href: "/dashboard/users",
  },
  {
    name: "Students",
    href: "/dashboard/students",
  },
  {
    name: "Lecturers",
    href: "/dashboard/lecturers",
  },
  {
    name: "Courses",
    href: "/dashboard/courses",
  },
  {
    name: "Attendance",
    href: "/dashboard/attendance",
  },
  {
    name: "Academic Setup",
    href: "/dashboard/academic-setup",
  },
];

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <aside className="fixed inset-y-0 left-0 z-40 hidden w-64 border-r bg-white lg:block">
      <div className="flex h-full flex-col">
        <div className="border-b px-6 py-5">
          <h1 className="text-lg font-bold text-zinc-900">
            Attendance Management
          </h1>

          <p className="mt-1 text-xs text-zinc-500">
            Administration
          </p>
        </div>

        <nav className="flex-1 space-y-1 overflow-y-auto p-4">
          {navigation.map((item) => {
            const isActive =
              item.href === "/dashboard"
                ? pathname === item.href
                : pathname.startsWith(item.href);

            return (
              <Link
                key={item.href}
                href={item.href}
                className={`block rounded-lg px-4 py-3 text-sm font-medium transition ${
                  isActive
                    ? "bg-blue-50 text-blue-700"
                    : "text-zinc-600 hover:bg-zinc-50 hover:text-zinc-900"
                }`}
              >
                {item.name}
              </Link>
            );
          })}
        </nav>
      </div>
    </aside>
  );
}