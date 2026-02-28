import React, {useEffect, useState} from 'react'
import CUPS from './components/CUPS'
import Examenes from './components/Examenes'
import Solicitudes from './components/Solicitudes'
import Facturas from './components/Facturas'
import Login from './components/Auth/Login'
import Register from './components/Auth/Register'
import Dashboard from './components/Dashboard'
import Rips from './components/Rips'
import Notifications from './components/Notifications'
import FacturasList from './components/FacturasList'

const API_BASE = (import.meta.env.VITE_API_URL ?? '').replace(/\/$/, '')

function Pacientes(){
  const [pacientes, setPacientes] = useState([])
  const [form, setForm] = useState({nombre:'', primerApellido:'', idTipoDocumento:'CC', nroIdent:'', fechaNacimiento:''})

  useEffect(()=>{ import('./api').then(m=>m.apiFetch('/api/pacientes').then(r=>r.json()).then(setPacientes)) },[])

  const submit = async e => {
    e.preventDefault()
    await (await import('./api')).apiFetch('/api/pacientes', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ IdTipoDocumento: form.idTipoDocumento, NroIdentificacion: form.nroIdent, Nombre: form.nombre, PrimerApellido: form.primerApellido, FechaNacimiento: form.fechaNacimiento }) })
    const res = await (await import('./api')).apiFetch('/api/pacientes'); setPacientes(await res.json())
  }

  return (
    <div>
      <h2>Pacientes</h2>
      <form onSubmit={submit} style={{marginBottom:12}}>
        <input placeholder="Nombres" value={form.nombre} onChange={e=>setForm({...form,nombre:e.target.value})} />
        <input placeholder="Primer Apellido" value={form.primerApellido} onChange={e=>setForm({...form,primerApellido:e.target.value})} />
        <input placeholder="Tipo Doc (CC)" value={form.idTipoDocumento} onChange={e=>setForm({...form,idTipoDocumento:e.target.value})} />
        <input placeholder="Nro Ident" value={form.nroIdent} onChange={e=>setForm({...form,nroIdent:e.target.value})} />
        <input type="date" value={form.fechaNacimiento} onChange={e=>setForm({...form,fechaNacimiento:e.target.value})} />
        <button type="submit">Crear</button>
      </form>
      <ul>
        {pacientes.map(p => (<li key={p.idPaciente}>{p.nombreCompleto} — {p.nroIdentificacion} — {p.nroHistoriaClinica}</li>))}
      </ul>
    </div>
  )
}

function Slots(){
  const [fecha, setFecha] = useState(new Date().toISOString().slice(0,10))
  const [codigoSede, setCodigoSede] = useState('PRINCIPAL')
  const [slots, setSlots] = useState([])
  const [publish, setPublish] = useState({cantidad:8, inicio: new Date().toISOString().slice(0,16), dur:30})

  const load = async ()=>{
    const res = await (await import('./api')).apiFetch(`/api/disponibilidad/slots?codigoSede=${encodeURIComponent(codigoSede)}&fecha=${fecha}`)
    setSlots(await res.json())
  }

  useEffect(()=>{ load() },[])

  const doPublish = async e => {
    e.preventDefault()
    await (await import('./api')).apiFetch('/api/disponibilidad/publish',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ CodigoSede: codigoSede, FechaInicio: publish.inicio, CantidadSlots: Number(publish.cantidad), DuracionMinutos: Number(publish.dur), CupoMaximoPorSlot:1 })})
    load()
  }

  const programar = async (slot) => {
    const idPaciente = prompt('IdPaciente para agendar')
    if (!idPaciente) return
    // RowVer is binary; backend expects byte[] via base64 string
    const body = { IdPaciente: Number(idPaciente), IdDisponibilidad: slot.idDisponibilidad, RowVerEsperado: slot.rowVer }
    const res = await (await import('./api')).apiFetch('/api/citas/programar',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(body)})
    if (res.status === 201) alert('Cita programada')
    else alert('Error al programar')
    load()
  }

  return (
    <div>
      <h2>Slots</h2>
      <form onSubmit={doPublish} style={{marginBottom:12}}>
        <label>Sede <input value={codigoSede} onChange={e=>setCodigoSede(e.target.value)} /></label>
        <label>Inicio <input type="datetime-local" value={publish.inicio} onChange={e=>setPublish({...publish,inicio:e.target.value})} /></label>
        <label>Cantidad <input type="number" value={publish.cantidad} onChange={e=>setPublish({...publish,cantidad:e.target.value})} /></label>
        <button type="submit">Publicar</button>
      </form>

      <div>
        <label>Fecha: <input type="date" value={fecha} onChange={e=>setFecha(e.target.value)} /></label>
        <button onClick={load}>Cargar</button>
      </div>

      <ul>
        {slots.map(s => (
          <li key={s.idDisponibilidad}>
            {new Date(s.fechaHora).toLocaleString()} — {s.cuposOcupados}/{s.cupoMaximo}
            <button onClick={()=>programar(s)} style={{marginLeft:8}}>Agendar</button>
            <div style={{fontSize:12,color:'#666'}}>RowVer: {btoa(String.fromCharCode.apply(null, new Uint8Array((s.rowVer instanceof ArrayBuffer)? new Uint8Array(s.rowVer): new Uint8Array(s.rowVer))))}</div>
          </li>
        ))}
      </ul>
    </div>
  )
}

export default function App(){
  const [tab, setTab] = useState('pacientes')
  const [auth, setAuth] = useState(() => ({ token: localStorage.getItem('lab_token'), username: localStorage.getItem('lab_user') }))

  const handleLogin = (payload) => {
    setAuth(payload)
    setTab('dashboard')
  }

  const handleLogout = () => {
    setAuth({ token: null, username: null })
    setTab('login')
  }

  return (
    <div style={{padding:20}}>
      <h1>Lab Frontend</h1>
      <div style={{marginBottom:12}}>
        <button onClick={()=>setTab('pacientes')}>Pacientes</button>
        <button onClick={()=>setTab('slots')} style={{marginLeft:8}}>Slots / Citas</button>
        <button onClick={()=>setTab('cups')} style={{marginLeft:8}}>CUPS</button>
        <button onClick={()=>setTab('examenes')} style={{marginLeft:8}}>Exámenes</button>
        <button onClick={()=>setTab('solicitudes')} style={{marginLeft:8}}>Solicitudes</button>
        <button onClick={()=>setTab('facturas')} style={{marginLeft:8}}>Facturas</button>
        <button onClick={()=>setTab('dashboard')} style={{marginLeft:8}}>Dashboard</button>
        <button onClick={()=>setTab('rips')} style={{marginLeft:8}}>RIPS</button>
        <button onClick={()=>setTab('notifications')} style={{marginLeft:8}}>Notifications</button>
        <button onClick={()=>setTab('facturas_list')} style={{marginLeft:8}}>Facturas (lista)</button>
        {!auth.token && <button onClick={()=>setTab('login')} style={{marginLeft:8}}>Login</button>}
        {!auth.token && <button onClick={()=>setTab('register')} style={{marginLeft:8}}>Register</button>}
      </div>
      {tab==='pacientes' && <Pacientes/>}
      {tab==='slots' && <Slots/>}
      {tab==='cups' && <CUPS apiBase={API_BASE}/>} 
      {tab==='examenes' && <Examenes apiBase={API_BASE}/>} 
      {tab==='solicitudes' && <Solicitudes apiBase={API_BASE}/>} 
      {tab==='facturas' && <Facturas apiBase={API_BASE}/>} 
      {tab==='login' && <Login onLogin={handleLogin}/>}
      {tab==='register' && <Register/>}
      {tab==='dashboard' && <Dashboard onLogout={handleLogout}/>}
      {tab==='rips' && <Rips />}
      {tab==='notifications' && <Notifications />}
      {tab==='facturas_list' && <FacturasList />}
    </div>
  )
}
