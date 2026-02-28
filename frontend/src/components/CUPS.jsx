import React, {useEffect, useState} from 'react'

export default function CUPS({apiBase}){
  const [cups, setCups] = useState([])
  const [form, setForm] = useState({codigo:'', descripcion:'', tipoServicio:''})

  useEffect(()=>{ import('../api').then(m=>m.apiFetch('/api/cups').then(r=>r.json()).then(setCups)) },[])

  const submit = async e => {
    e.preventDefault()
    await (await import('../api')).apiFetch('/api/cups',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ CodigoCUPS: form.codigo, Descripcion: form.descripcion, TipoServicio: form.tipoServicio })})
    const res = await (await import('../api')).apiFetch('/api/cups'); setCups(await res.json())
    setForm({codigo:'', descripcion:'', tipoServicio:''})
  }

  return (
    <div>
      <h2>CUPS</h2>
      <form onSubmit={submit} style={{marginBottom:12}}>
        <input placeholder="Código" value={form.codigo} onChange={e=>setForm({...form,codigo:e.target.value})} />
        <input placeholder="Descripción" value={form.descripcion} onChange={e=>setForm({...form,descripcion:e.target.value})} />
        <input placeholder="Tipo Servicio" value={form.tipoServicio} onChange={e=>setForm({...form,tipoServicio:e.target.value})} />
        <button type="submit">Crear CUPS</button>
      </form>
      <ul>
        {cups.map(c => (<li key={c.idCUPS}>{c.codigoCUPS || c.codigoCUPS || c.CodigoCUPS} — {c.descripcion || c.Descripcion || c.Descripcion}</li>))}
      </ul>
    </div>
  )
}
