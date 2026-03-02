const API_BASE_URL = (import.meta.env.VITE_API_BASE_URL as string) || 'http://localhost:5000/api'

interface RequestOptions extends RequestInit {
  token?: string
}

export async function apiCall(
  endpoint: string,
  options: RequestOptions = {}
): Promise<Response> {
  const { token, ...restOptions } = options
  const url = `${API_BASE_URL}${endpoint}`

  const headers: Record<string, string> = {
    'Content-Type': 'application/json',
    ...(typeof restOptions.headers === 'object' && restOptions.headers ? restOptions.headers : {}),
  }

  if (token) {
    headers['Authorization'] = `Bearer ${token}`
  }

  const response = await fetch(url, {
    ...restOptions,
    headers,
  })

  if (!response.ok) {
    throw new Error(`API error: ${response.statusText}`)
  }

  return response
}

export async function apiGet(endpoint: string, token?: string) {
  const response = await apiCall(endpoint, {
    method: 'GET',
    token,
  })
  return response.json()
}

export async function apiPost(
  endpoint: string,
  data: any,
  token?: string
) {
  const response = await apiCall(endpoint, {
    method: 'POST',
    body: JSON.stringify(data),
    token,
  })
  return response.json()
}

export async function apiPut(
  endpoint: string,
  data: any,
  token?: string
) {
  const response = await apiCall(endpoint, {
    method: 'PUT',
    body: JSON.stringify(data),
    token,
  })
  return response.json()
}

export async function apiDelete(endpoint: string, token?: string) {
  const response = await apiCall(endpoint, {
    method: 'DELETE',
    token,
  })
  return response.json()
}
