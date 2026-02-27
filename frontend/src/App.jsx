import React, {useEffect, useState} from 'react'

export default function App(){
  const [pacientes, setPacientes] = useState([])
  const [form, setForm] = useState({nombre:'', primerApellido:'', idTipoDocumento:'CC', nroIdent:'', fechaNacimiento:''})

  useEffect(()=>{
    fetch('/api/pacientes').then(r=>r.json()).then(setPacientes)
  },[])

  const submit = async e => {
    e.preventDefault()
    await fetch('/api/pacientes',{
      method:'POST', headers:{'Content-Type':'application/json'},
      body: JSON.stringify({
        IdTipoDocumento: form.idTipoDocumento,
        NroIdentificacion: form.nroIdent,
        Nombre: form.nombre,
        PrimerApellido: form.primerApellido,
        FechaNacimiento: form.fechaNacimiento
      })
    })
    const res = await fetch('/api/pacientes'); setPacientes(await res.json())
  }

  return (
    <div style={{padding:20}}>
      <h1>Pacientes</h1>
      <form onSubmit={submit} style={{marginBottom:20}}>
        <input placeholder="Nombres" value={form.nombre} onChange={e=>setForm({...form,nombre:e.target.value})} />
        <input placeholder="Primer Apellido" value={form.primerApellido} onChange={e=>setForm({...form,primerApellido:e.target.value})} />
        <input placeholder="Tipo Doc (CC)" value={form.idTipoDocumento} onChange={e=>setForm({...form,idTipoDocumento:e.target.value})} />
        <input placeholder="Nro Ident" value={form.nroIdent} onChange={e=>setForm({...form,nroIdent:e.target.value})} />
        <input type="date" value={form.fechaNacimiento} onChange={e=>setForm({...form,fechaNacimiento:e.target.value})} />
        <button type="submit">Crear</button>
      </form>

      <ul>
        {pacientes.map(p => (
          <li key={p.idPaciente}>{p.nombreCompleto} — {p.nroIdentificacion} — {p.nroHistoriaClinica}</li>
        ))}
      </ul>
    </div>
  )
}
