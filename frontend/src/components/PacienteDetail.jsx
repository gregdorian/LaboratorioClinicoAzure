import React, {useEffect, useState} from 'react'

export default function PacienteDetail({id}){
  const [paciente, setPaciente] = useState(null)

  useEffect(()=>{
    if (!id) return
    (async ()=>{
      const res = await (await import('../api')).apiFetch(`/api/pacientes/${id}`)
      if (res.ok) setPaciente(await res.json())
    })()
  },[id])

  if (!id) return <div>Seleccione un paciente</div>
  if (!paciente) return <div>Cargando...</div>
  return (
    <div>
      <h3>{paciente.nombreCompleto || paciente.nombre}</h3>
      <div>Identificación: {paciente.nroIdentificacion}</div>
      <div>Fecha Nac: {paciente.fechaNacimiento}</div>
    </div>
  )
}
