import React, {useState} from 'react'

const API_BASE = (import.meta.env.VITE_API_URL ?? '').replace(/\/$/, '')

export default function Login({onLogin}){
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState(null)

  const submit = async e => {
    e.preventDefault()
    setError(null)
    const res = await fetch(`${API_BASE}/api/auth/login`, { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify({ username, password }) })
    if (res.ok){
      const body = await res.json()
      localStorage.setItem('lab_token', body.token)
      localStorage.setItem('lab_user', body.username)
      onLogin && onLogin({ token: body.token, username: body.username })
    } else {
      const txt = await res.text()
      setError(txt || 'Login failed')
    }
  }

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={submit}>
        <input placeholder="Username" value={username} onChange={e=>setUsername(e.target.value)} />
        <input placeholder="Password" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
        <button type="submit">Login</button>
      </form>
      {error && <div style={{color:'red'}}>{error}</div>}
    </div>
  )
}
