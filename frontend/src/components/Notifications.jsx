import React, {useState} from 'react'

export default function Notifications(){
  const [to, setTo] = useState('')
  const [message, setMessage] = useState('')
  const [status, setStatus] = useState(null)

  const enqueue = async () => {
    setStatus('Encolando...')
    const body = { Destinatario: to, Asunto: 'Manual', MensajeError: message, TipoNotif: 'EMAIL' }
    const res = await (await import('../api')).apiFetch('/api/notifications/enqueue', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(body) })
    if (res.status===201) setStatus('Encolado')
    else setStatus('Error')
  }

  const process = async () => {
    setStatus('Procesando...')
    const res = await (await import('../api')).apiFetch('/api/notifications/process?max=50', { method:'POST' })
    if (res.ok){
      const b = await res.json()
      setStatus('Procesados: ' + b.processed)
    } else setStatus('Error procesando')
  }

  return (
    <div>
      <h2>Notifications</h2>
      <div>
        <input placeholder="Destinatario" value={to} onChange={e=>setTo(e.target.value)} />
        <input placeholder="Mensaje" value={message} onChange={e=>setMessage(e.target.value)} style={{marginLeft:8}} />
        <button onClick={enqueue} style={{marginLeft:8}}>Enqueue</button>
        <button onClick={process} style={{marginLeft:8}}>Process</button>
      </div>
      {status && <div style={{marginTop:8}}>{status}</div>}
    </div>
  )
}
