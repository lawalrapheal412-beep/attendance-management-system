export interface LoginResponse {
  userId: string;
  fullName: string;
  email: string;
  role: number | string;
  token: string;
}

const API_BASE = "/api/backend";

export interface CreateUserRequest {
  fullName: string;
  email: string;
  role: number;
}

export async function createUser(
  token: string,
  data: CreateUserRequest,
): Promise<unknown> {
  const response = await fetch(`${API_BASE}/users`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    if (response.status === 400) {
      const message = await response.text();

      throw new Error(
        message
          ? message.replace(/^"|"$/g, "")
          : "Invalid user information.",
      );
    }

    if (response.status === 401 || response.status === 403) {
      throw new Error("You are not authorized to create users.");
    }

    throw new Error("Unable to create user.");
  }

  return response.json();
}

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

export interface UserSummary {
  id: string;
  fullName: string;
  email: string;
  role: number;
  isActive: boolean;
}

export async function getUserById(
  token: string,
  id: string,
): Promise<UserSummary> {
  const response = await fetch(`${API_BASE}/users/${id}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
    cache: "no-store",
  });

  if (!response.ok) {
    if (response.status === 404) {
      throw new Error("User not found.");
    }

    if (response.status === 401 || response.status === 403) {
      throw new Error("You are not authorized to view this user.");
    }

    throw new Error("Unable to load user.");
  }

  return response.json();
}

export interface UpdateUserRequest {
  id: string;
  fullName: string;
  email: string;
  role: number;
  isActive: boolean;
}
export async function updateUser(
  token: string,
  id: string,
  data: UpdateUserRequest,
): Promise<UserSummary> {
  const response = await fetch(`${API_BASE}/users/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    if (response.status === 400) {
      const message = await response.text();

      throw new Error(
        message
          ? message.replace(/^"|"$/g, "")
          : "Invalid user information.",
      );
    }

    if (response.status === 404) {
      throw new Error("User not found.");
    }

    if (response.status === 401 || response.status === 403) {
      throw new Error(
        "You are not authorized to update this user.",
      );
    }

    throw new Error("Unable to update user.");
  }
  if (response.status === 204) {
    return getUserById(token, id);
  }
  return response.json();
}

export async function deleteUser(
  token: string,
  id: string,
): Promise<void> {
  const response = await fetch(`${API_BASE}/users/${id}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    if (response.status === 404) {
      throw new Error("User not found.");
    }

    if (response.status === 401 || response.status === 403) {
      throw new Error(
        "You are not authorized to delete this user.",
      );
    }

    throw new Error("Unable to delete user.");
  }
}
export async function resendPasswordSetup(
  token: string,
  id: string,
): Promise<void> {
  const response = await fetch(
    `${API_BASE}/users/${id}/resend-password-setup`,
    {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  );

  if (!response.ok) {
    if (response.status === 400) {
      const message = await response.text();

      throw new Error(
        message
          ? message.replace(/^"|"$/g, "")
          : "Unable to resend password setup link.",
      );
    }

    if (response.status === 401 || response.status === 403) {
      throw new Error(
        "You are not authorized to resend password setup links.",
      );
    }

    if (response.status === 404) {
      throw new Error("User not found.");
    }

    throw new Error(
      "Unable to resend password setup link.",
    );
  }
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