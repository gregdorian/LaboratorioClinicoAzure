using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.Queries;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly LabDbContext _db;
        
        public CitasController(IDispatcher dispatcher, LabDbContext db)
        {
            _dispatcher = dispatcher;
            _db = db;
        }

        /// <summary>
        /// GET api/citas - Listar todas las citas (Admin)
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var citas = _db.Citas
                .Select(c => new CitaDetailDto
                {
                    IdCita = c.IdCita,
                    IdPaciente = c.IdPaciente,
                    NombrePaciente = c.Paciente != null && c.Paciente.Persona != null 
                        ? c.Paciente.Persona.Nombre 
                        : "Sin Nombre",
                    FechaHora = c.Disponibilidad != null ? c.Disponibilidad.FechaHora : DateTime.MinValue,
                    Estado = c.IdEstadoCita == 1 ? "Programada" : c.IdEstadoCita == 2 ? "Confirmada" : "Cancelada",
                    Motivo = c.Motivo,
                    FechaRegistro = c.FechaRegistro,
                    ObservacionesPaciente = c.ObservacionesPac
                })
                .ToList();

            return Ok(citas);
        }

        /// <summary>
        /// GET api/citas/{id} - Obtener cita específica
        /// </summary>
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var cita = _db.Citas
                .Where(c => c.IdCita == id)
                .Select(c => new CitaDetailDto
                {
                    IdCita = c.IdCita,
                    IdPaciente = c.IdPaciente,
                    NombrePaciente = c.Paciente != null && c.Paciente.Persona != null 
                        ? c.Paciente.Persona.Nombre 
                        : "Sin Nombre",
                    FechaHora = c.Disponibilidad != null ? c.Disponibilidad.FechaHora : DateTime.MinValue,
                    Estado = c.IdEstadoCita == 1 ? "Programada" : c.IdEstadoCita == 2 ? "Confirmada" : "Cancelada",
                    Motivo = c.Motivo,
                    FechaRegistro = c.FechaRegistro,
                    ObservacionesPaciente = c.ObservacionesPac
                })
                .FirstOrDefault();

            if (cita == null) return NotFound();
            return Ok(cita);
        }

        /// <summary>
        /// GET api/citas/paciente/{pacienteId} - Obtener citas de un paciente
        /// </summary>
        [HttpGet("paciente/{pacienteId:long}")]
        public IActionResult GetByPaciente(long pacienteId)
        {
            var citas = _db.Citas
                .Where(c => c.IdPaciente == pacienteId)
                .Select(c => new CitaDetailDto
                {
                    IdCita = c.IdCita,
                    IdPaciente = c.IdPaciente,
                    NombrePaciente = c.Paciente != null && c.Paciente.Persona != null 
                        ? c.Paciente.Persona.Nombre 
                        : "Sin Nombre",
                    FechaHora = c.Disponibilidad != null ? c.Disponibilidad.FechaHora : DateTime.MinValue,
                    Estado = c.IdEstadoCita == 1 ? "Programada" : c.IdEstadoCita == 2 ? "Confirmada" : "Cancelada",
                    Motivo = c.Motivo,
                    FechaRegistro = c.FechaRegistro,
                    ObservacionesPaciente = c.ObservacionesPac
                })
                .ToList();

            return Ok(citas);
        }

        [HttpPost("programar")]
        public async Task<IActionResult> Programar(ProgramarCitaCommand command)
        {
            var id = await _dispatcher.Send<ProgramarCitaCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
