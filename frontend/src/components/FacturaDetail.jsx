import React, {useEffect, useState} from 'react'

export default function FacturaDetail({id, onClose}){
  const [factura, setFactura] = useState(null)
  useEffect(()=>{
    if (!id) return
    (async ()=>{
      const res = await (await import('../api')).apiFetch(`/api/facturas/${id}`)
      if (res.ok) setFactura(await res.json())
      else setFactura(null)
    })()
  },[id])

  if (!id) return null
  if (!factura) return <div>Cargando factura...</div>
  return (
    <div style={{border:'1px solid #ddd', padding:12, marginTop:8}}>
      <button onClick={onClose} style={{float:'right'}}>Cerrar</button>
      <h3>Factura {factura.nroFactura || factura.NroFactura}</h3>
      <div>ID: {factura.idFactura || factura.IdFactura}</div>
      <div>Solicitud: {factura.idSolicitud || factura.IdSolicitud}</div>
      <div>Entidad: {factura.idEntidadPagadora || factura.IdEntidadPagadora}</div>
      <div>Monto: {factura.montoTotal || factura.MontoTotal}</div>
      <div>Estado: {factura.estadoPago || factura.EstadoPago}</div>
      <div>Fecha: {factura.fechaFacturacion || factura.FechaFacturacion}</div>
    </div>
  )
}
