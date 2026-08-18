export interface LoginResponse {
  userId: string;
  fullName: string;
  email: string;
  role: number | string;
  token: string;
}

const API_BASE = "/api/backend";

export async function login(
  email: string,
  password: string,
): Promise<LoginResponse> {
  const response = await fetch(`${API_BASE}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email,
      password,
    }),
  });

  if (!response.ok) {
    if (response.status === 401) {
      throw new Error("Invalid email or password.");
    }

    throw new Error("Unable to sign in. Please try again.");
  }

  return response.json();
}

export async function getUsers(
  token: string,
): Promise<unknown[]> {
  const response = await fetch(`${API_BASE}/users`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
    cache: "no-store",
  });

  if (!response.ok) {
    throw new Error("Unable to load users.");
  }

  return response.json();
}