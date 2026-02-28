import React, {useState} from 'react'

export default function Rips(){
  const [numeroLote, setNumeroLote] = useState('')
  const [generarMsg, setGenerarMsg] = useState(null)
  const [exportUrl, setExportUrl] = useState(null)

  const generar = async () => {
    setGenerarMsg('Generando...')
    const res = await (await import('../api')).apiFetch('/api/rips/generar', { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify({ Usuario: localStorage.getItem('lab_user') || 'web' }) })
    if (res.ok) {
      const body = await res.json()
      setGenerarMsg('Generado: ' + JSON.stringify(body))
    } else {
      setGenerarMsg('Error generando')
    }
  }

  const generarLote = async () => {
    setGenerarMsg('Generando lote...')
    const res = await (await import('../api')).apiFetch('/api/rips/generar-lote', { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify({ Usuario: localStorage.getItem('lab_user') || 'web' }) })
    if (res.ok){
      const body = await res.json()
      setGenerarMsg('Lote creado: ' + body.id)
    } else setGenerarMsg('Error generando lote')
  }

  const exportar = async () => {
    if (!numeroLote) return alert('Ingrese numero de lote')
    const res = await (await import('../api')).apiFetch(`/api/rips/export?numeroLote=${encodeURIComponent(numeroLote)}`)
    if (res.ok){
      const blob = await res.blob()
      const url = URL.createObjectURL(blob)
      setExportUrl(url)
    } else alert('No encontrado o error')
  }

  return (
    <div>
      <h2>RIPS</h2>
      <div style={{marginBottom:8}}>
        <button onClick={generar}>Generar RIPS Transaccional</button>
        <button onClick={generarLote} style={{marginLeft:8}}>Generar Lote RIPS</button>
      </div>
      <div style={{marginTop:12}}>
        <input placeholder="Número de lote" value={numeroLote} onChange={e=>setNumeroLote(e.target.value)} />
        <button onClick={exportar} style={{marginLeft:8}}>Exportar CSV</button>
        {exportUrl && <a href={exportUrl} download={numeroLote + '.csv'} style={{marginLeft:8}}>Descargar</a>}
      </div>
      {generarMsg && <div style={{marginTop:12}}>{generarMsg}</div>}
    </div>
  )
}
