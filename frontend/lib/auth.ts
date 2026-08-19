export interface AuthUser {
  userId: string;
  fullName: string;
  email: string;
  role: number | string;
}

const TOKEN_KEY = "ams_token";
const USER_KEY = "ams_user";

export function getToken(): string | null {
  if (typeof window === "undefined") {
    return null;
  }

  return sessionStorage.getItem(TOKEN_KEY);
}

export function getCurrentUser(): AuthUser | null {
  if (typeof window === "undefined") {
    return null;
  }

  const storedUser = sessionStorage.getItem(USER_KEY);

  if (!storedUser) {
    return null;
  }

  try {
    return JSON.parse(storedUser) as AuthUser;
  } catch {
    return null;
  }
}

export function setAuthSession(user: AuthUser & { token: string }): void {
  if (typeof window === "undefined") {
    return;
  }

  sessionStorage.setItem(TOKEN_KEY, user.token);

  sessionStorage.setItem(
    USER_KEY,
    JSON.stringify({
      userId: user.userId,
      fullName: user.fullName,
      email: user.email,
      role: user.role,
    }),
  );
}

export function isAuthenticated(): boolean {
  return getToken() !== null && getCurrentUser() !== null;
}

export function isAdmin(): boolean {
  const user = getCurrentUser();

  if (!user) {
    return false;
  }

  return String(user.role).toLowerCase() === "admin" ||
         String(user.role) === "0";
}

export function logout(): void {
  if (typeof window === "undefined") {
    return;
  }

  sessionStorage.removeItem(TOKEN_KEY);
  sessionStorage.removeItem(USER_KEY);
}