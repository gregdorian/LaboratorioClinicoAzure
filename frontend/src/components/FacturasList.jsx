import React, {useEffect, useState} from 'react'
import FacturaDetail from './FacturaDetail'

export default function FacturasList(){
  const [facturas, setFacturas] = useState([])
  const [selected, setSelected] = useState(null)

  useEffect(()=>{
    (async ()=>{
      const res = await (await import('../api')).apiFetch('/api/facturas')
      if (res.ok) setFacturas(await res.json())
    })()
  },[])

  return (
    <div>
      <h2>Facturas</h2>
      <ul>
        {facturas.map(f=> (
          <li key={f.idFactura} style={{cursor:'pointer'}} onClick={()=>setSelected(f.idFactura)}>
            {f.nroFactura || f.NroFactura} — {f.montoTotal || f.MontoTotal}
          </li>
        ))}
      </ul>
      {selected && <FacturaDetail id={selected} onClose={()=>setSelected(null)} />}
    </div>
  )
}
