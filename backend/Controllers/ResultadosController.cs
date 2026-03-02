using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ResultadosController : ControllerBase
    {
        private readonly LabDbContext _db;

        public ResultadosController(LabDbContext db) => _db = db;

        /// <summary>
        /// GET api/resultados/buscar - Búsqueda de resultados por paciente
        /// </summary>
        [HttpGet("buscar")]
        public IActionResult Buscar([FromQuery] string nroIdentificacion, [FromQuery] string apellido)
        {
            if (string.IsNullOrEmpty(nroIdentificacion) && string.IsNullOrEmpty(apellido))
                return BadRequest("Debe proporcionar número de identificación o apellido");

            var resultados = _db.Resultados
                .Join(_db.ExamenMuestra, r => r.IdExamenMuestra, m => m.IdExamenMuestra, (r, m) => new { Resultado = r, Muestra = m })
                .Join(_db.SolicitudesExamen, rm => rm.Muestra.IdExamenSolicitado, s => s.IdSolicitud, (rm, s) => new { rm.Resultado, rm.Muestra, Solicitud = s })
                .Join(_db.Paciente, rms => rms.Solicitud.IdPaciente, p => p.IdPaciente, (rms, p) => new { rms.Resultado, rms.Muestra, rms.Solicitud, Paciente = p })
                .Join(_db.Persona, p => p.Paciente.IdPersona, per => per.IdPersona, (p, per) => new { p.Resultado, p.Muestra, p.Solicitud, p.Paciente, Persona = per })
                .Where(x => string.IsNullOrEmpty(nroIdentificacion) || x.Persona.NroIdentificacion.Contains(nroIdentificacion))
                .Where(x => string.IsNullOrEmpty(apellido) || x.Persona.PrimerApellido.Contains(apellido))
                .Select(x => new ResultadoSearchDto
                {
                    IdResultado = x.Resultado.IdResultado,
                    NombrePaciente = x.Persona.Nombre ?? "Sin Nombre",
                    ApellidoPaciente = x.Persona.PrimerApellido ?? "Sin Apellido",
                    NroIdentificacion = x.Persona.NroIdentificacion,
                    FechaAnalisis = x.Resultado.FechaResultado,
                    Resultado = x.Resultado.ResultadoValor,
                    Observaciones = x.Resultado.ResultadoTexto
                })
                .ToList();

            if (!resultados.Any()) return NotFound("No se encontraron resultados");
            return Ok(resultados);
        }

        /// <summary>
        /// GET api/resultados/paciente/{pacienteId} - Obtener resultados de un paciente específico
        /// </summary>
        [HttpGet("paciente/{pacienteId:long}")]
        [Authorize]
        public IActionResult GetPorPaciente(long pacienteId)
        {
            var resultados = _db.Resultados
                .Join(_db.ExamenMuestra, r => r.IdExamenMuestra, m => m.IdExamenMuestra, (r, m) => new { Resultado = r, Muestra = m })
                .Where(rm => rm.Resultado.IdExamenSolicitado == rm.Resultado.IdExamenSolicitado)
                .Select(r => new ResultadoDetailDto
                {
                    IdResultado = r.Resultado.IdResultado,
                    IdMuestra = r.Resultado.IdExamenMuestra,
                    TipoMuestra = "Muestra Estándar",
                    Resultado = r.Resultado.ResultadoValor,
                    Unidad = r.Resultado.Unidad ?? "N/A",
                    RangoReferencia = "N/A",
                    Estado = r.Resultado.EstaFueraRango ? "Anormal" : "Normal",
                    FechaAnalisis = r.Resultado.FechaResultado,
                    Observaciones = r.Resultado.ResultadoTexto
                })
                .ToList();

            return Ok(resultados);
        }

        /// <summary>
        /// GET api/resultados/{id} - Obtener resultado específico
        /// </summary>
        [HttpGet("{id:long}")]
        [Authorize]
        public IActionResult Get(long id)
        {
            var resultado = _db.Resultados
                .Where(r => r.IdResultado == id)
                .Select(r => new ResultadoDetailDto
                {
                    IdResultado = r.IdResultado,
                    IdMuestra = r.IdExamenMuestra,
                    TipoMuestra = "Muestra Estándar",
                    Resultado = r.ResultadoValor,
                    Unidad = r.Unidad ?? "N/A",
                    RangoReferencia = "N/A",
                    Estado = r.EstaFueraRango ? "Anormal" : "Normal",
                    FechaAnalisis = r.FechaResultado,
                    Observaciones = r.ResultadoTexto
                })
                .FirstOrDefault();

            if (resultado == null) return NotFound();
            return Ok(resultado);
        }
    }
}
