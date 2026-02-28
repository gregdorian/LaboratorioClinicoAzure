import React, {useState} from 'react'

export default function Facturas({apiBase}){
  const [form, setForm] = useState({idSolicitud:'', idEntidad:'', monto:'', fecha:new Date().toISOString().slice(0,10)})

  const submit = async e => {
    e.preventDefault()
    const body = { IdSolicitud: Number(form.idSolicitud), IdEntidadPagadora: Number(form.idEntidad), MontoTotal: Number(form.monto), FechaFacturacion: new Date(form.fecha).toISOString() }
    const res = await (await import('./api')).apiFetch('/api/facturas',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(body)})
    if (res.status===201) { alert('Factura creada'); setForm({idSolicitud:'', idEntidad:'', monto:'', fecha:new Date().toISOString().slice(0,10)}) }
    else alert('Error creando factura')
  }

  return (
    <div>
      <h2>Facturas</h2>
      <form onSubmit={submit} style={{marginBottom:12}}>
        <input placeholder="Id Solicitud" value={form.idSolicitud} onChange={e=>setForm({...form,idSolicitud:e.target.value})} />
        <input placeholder="Id Entidad Pagadora" value={form.idEntidad} onChange={e=>setForm({...form,idEntidad:e.target.value})} />
        <input placeholder="Monto Total" type="number" value={form.monto} onChange={e=>setForm({...form,monto:e.target.value})} />
        <input type="date" value={form.fecha} onChange={e=>setForm({...form,fecha:e.target.value})} />
        <button type="submit">Crear Factura</button>
      </form>
    </div>
  )
}
