const API_BASE = (import.meta.env.VITE_API_URL ?? '').replace(/\/$/, '')

export async function apiFetch(path, options = {}){
  const token = localStorage.getItem('lab_token')
  const headers = Object.assign({}, options.headers || {})
  if (token) headers['Authorization'] = `Bearer ${token}`
  const res = await fetch(`${API_BASE}${path}`, Object.assign({}, options, { headers }))
  return res
}

export default API_BASE
