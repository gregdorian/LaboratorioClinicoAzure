import React, {useEffect, useState} from 'react'

export default function Solicitudes({apiBase}){
  const [pacientes, setPacientes] = useState([])
  const [examenes, setExamenes] = useState([])
  const [items, setItems] = useState([])
  const [form, setForm] = useState({idPaciente:'', idMedico:'', fechaSolicitud:new Date().toISOString().slice(0,10)})

  useEffect(()=>{
    import('../api').then(m=>{
      m.apiFetch('/api/pacientes').then(r=>r.json()).then(setPacientes)
      m.apiFetch('/api/examenes').then(r=>r.json()).then(setExamenes)
    })
  },[])

  const addItem = () => setItems([...items, {idExamen:'', cantidad:1, valorUnitario:0}])
  const updateItem = (idx, key, value) => { const copy = [...items]; copy[idx][key]=value; setItems(copy) }
  const removeItem = idx => { const copy=[...items]; copy.splice(idx,1); setItems(copy) }

  const submit = async e => {
    e.preventDefault()
    const body = {
      IdPaciente: Number(form.idPaciente),
      IdMedico: form.idMedico ? Number(form.idMedico) : null,
      FechaSolicitud: new Date(form.fechaSolicitud).toISOString(),
      Examenes: items.map(i=>({ IdExamen: Number(i.idExamen), Cantidad: Number(i.cantidad), ValorUnitario: Number(i.valorUnitario) }))
    }
    const res = await (await import('../api')).apiFetch('/api/solicitudes',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(body)})
    if (res.status===201) { alert('Solicitud creada'); setItems([]); setForm({idPaciente:'', idMedico:'', fechaSolicitud:new Date().toISOString().slice(0,10)}) }
    else { alert('Error creando solicitud') }
  }

  return (
    <div>
      <h2>Crear Solicitud</h2>
      <form onSubmit={submit} style={{marginBottom:12}}>
        <label>Paciente: <select value={form.idPaciente} onChange={e=>setForm({...form,idPaciente:e.target.value})}>
          <option value="">-- seleccionar --</option>
          {pacientes.map(p => (<option key={p.idPaciente} value={p.idPaciente}>{p.nombreCompleto || p.nombreCompleto || p.NombreCompleto} — {p.nroIdentificacion}</option>))}
        </select></label>
        <label style={{marginLeft:8}}>Medico Id: <input value={form.idMedico} onChange={e=>setForm({...form,idMedico:e.target.value})} /></label>
        <label style={{marginLeft:8}}>Fecha: <input type="date" value={form.fechaSolicitud} onChange={e=>setForm({...form,fechaSolicitud:e.target.value})} /></label>

        <div style={{marginTop:12}}>
          <h4>Exámenes</h4>
          <button type="button" onClick={addItem}>Agregar examen</button>
          {items.map((it,idx)=>(
            <div key={idx} style={{marginTop:8}}>
              <select value={it.idExamen} onChange={e=>updateItem(idx,'idExamen',e.target.value)}>
                <option value="">-- seleccionar examen --</option>
                {examenes.map(x => (<option key={x.idExamen} value={x.idExamen}>{x.nombreExamen}</option>))}
              </select>
              <input type="number" value={it.cantidad} onChange={e=>updateItem(idx,'cantidad',e.target.value)} style={{width:80,marginLeft:8}} />
              <input type="number" value={it.valorUnitario} onChange={e=>updateItem(idx,'valorUnitario',e.target.value)} style={{width:120,marginLeft:8}} />
              <button type="button" onClick={()=>removeItem(idx)} style={{marginLeft:8}}>Quitar</button>
            </div>
          ))}
        </div>

        <div style={{marginTop:12}}>
          <button type="submit">Crear Solicitud</button>
        </div>
      </form>
    </div>
  )
}
