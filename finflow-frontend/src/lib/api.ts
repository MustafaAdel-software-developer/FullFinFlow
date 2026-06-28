const API_BASE = import.meta.env.VITE_API_URL ?? '/api';

export type AuthResponse = {
  message: string;
  token: string;
  refreshToken: string;
};

export type HealthResponse = {
  status: string;
  database: string;
};

async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(options?.headers ?? {}),
    },
    ...options,
  });

  if (!response.ok) {
    const error = await response.json().catch(() => ({ message: response.statusText }));
    throw new Error(error.message ?? 'Request failed');
  }

  return response.json() as Promise<T>;
}

export const api = {
  health: () => request<HealthResponse>('/health'),
  login: (email: string, password: string) =>
    request<AuthResponse>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password }),
    }),
  register: (payload: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    country: string;
    city: string;
    zipCode: string;
    addressLine: string;
  }) =>
    request<AuthResponse>('/auth/register', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
};

export function saveAuthToken(token: string) {
  localStorage.setItem('finflow_token', token);
}

export function getAuthToken() {
  return localStorage.getItem('finflow_token');
}

export function clearAuthToken() {
  localStorage.removeItem('finflow_token');
}
