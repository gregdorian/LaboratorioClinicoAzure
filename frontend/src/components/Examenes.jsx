import React, {useEffect, useState} from 'react'

export default function Examenes({apiBase}){
  const [examenes, setExamenes] = useState([])
  const [form, setForm] = useState({codigo:'', nombre:''})

  useEffect(()=>{ import('../api').then(m=>m.apiFetch('/api/examenes').then(r=>r.json()).then(setExamenes)) },[])

  const submit = async e => {
    e.preventDefault()
    await (await import('../api')).apiFetch('/api/examenes',{method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ CodigoExamen: form.codigo, NombreExamen: form.nombre })})
    const res = await (await import('../api')).apiFetch('/api/examenes'); setExamenes(await res.json())
    setForm({codigo:'', nombre:''})
  }

  return (
    <div>
      <h2>Exámenes</h2>
      <form onSubmit={submit} style={{marginBottom:12}}>
        <input placeholder="Código examen" value={form.codigo} onChange={e=>setForm({...form,codigo:e.target.value})} />
        <input placeholder="Nombre" value={form.nombre} onChange={e=>setForm({...form,nombre:e.target.value})} />
        <button type="submit">Crear Examen</button>
      </form>
      <ul>
        {examenes.map(x => (<li key={x.idExamen}>{x.codigoExamen || x.CodigoExamen} — {x.nombreExamen || x.NombreExamen}</li>))}
      </ul>
    </div>
  )
}
