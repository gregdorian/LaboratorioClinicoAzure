import React, {useState} from 'react'

const API_BASE = (import.meta.env.VITE_API_URL ?? '').replace(/\/$/, '')

export default function Register(){
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [email, setEmail] = useState('')
  const [message, setMessage] = useState(null)

  const submit = async e => {
    e.preventDefault()
    setMessage(null)
    const res = await fetch(`${API_BASE}/api/auth/register`, { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ username, password, email }) })
    if (res.status === 201){
      setMessage('Registered successfully')
    } else {
      const txt = await res.text()
      setMessage(txt || 'Error')
    }
  }

  return (
    <div>
      <h2>Register</h2>
      <form onSubmit={submit}>
        <input placeholder="Username" value={username} onChange={e=>setUsername(e.target.value)} />
        <input placeholder="Email" value={email} onChange={e=>setEmail(e.target.value)} />
        <input placeholder="Password" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
        <button type="submit">Register</button>
      </form>
      {message && <div>{message}</div>}
    </div>
  )
}
