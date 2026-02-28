import React, {useEffect, useState} from 'react'

const API_BASE = (import.meta.env.VITE_API_URL ?? '').replace(/\/$/, '')

export default function Dashboard({onLogout}){
  const [user, setUser] = useState(localStorage.getItem('lab_user'))
  const [serverInfo, setServerInfo] = useState(null)

  useEffect(()=>{
    import('./api').then(m=>{
      m.apiFetch('/api/pacientes').then(r=>r.ok? r.json(): null).then(d=>setServerInfo(d?.length ? `Pacientes: ${d.length}` : 'Authenticated')).catch(()=>setServerInfo('Authenticated'))
    })
  },[])

  const logout = ()=>{
    localStorage.removeItem('lab_token')
    localStorage.removeItem('lab_user')
    onLogout && onLogout()
  }

  return (
    <div>
      <h2>Dashboard</h2>
      <div>Usuario: {user}</div>
      <div>{serverInfo ?? 'Conectando...'}</div>
      <button onClick={logout}>Logout</button>
    </div>
  )
}
