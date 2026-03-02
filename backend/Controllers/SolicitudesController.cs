using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
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
    public class SolicitudesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly LabDbContext _db;

        public SolicitudesController(IDispatcher dispatcher, LabDbContext db)
        {
            _dispatcher = dispatcher;
            _db = db;
        }

        /// <summary>
        /// GET api/solicitudes - Listar todas las solicitudes
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var solicitudes = _db.SolicitudesExamen
                .Select(s => new SolicitudDto
                {
                    IdSolicitud = s.IdSolicitud,
                    NroOrden = s.NroOrden,
                    IdPaciente = s.IdPaciente,
                    FechaSolicitud = s.FechaSolicitud,
                    IsAnulado = s.IsAnulado,
                    CantidadExamenes = s.Examenes != null ? s.Examenes.Count : 0
                })
                .ToList();

            return Ok(solicitudes);
        }

        /// <summary>
        /// GET api/solicitudes/{id} - Obtener solicitud específica
        /// </summary>
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var solicitud = _db.SolicitudesExamen
                .Where(s => s.IdSolicitud == id)
                .Select(s => new SolicitudDto
                {
                    IdSolicitud = s.IdSolicitud,
                    NroOrden = s.NroOrden,
                    IdPaciente = s.IdPaciente,
                    FechaSolicitud = s.FechaSolicitud,
                    IsAnulado = s.IsAnulado,
                    CantidadExamenes = s.Examenes != null ? s.Examenes.Count : 0
                })
                .FirstOrDefault();

            if (solicitud == null) return NotFound();
            return Ok(solicitud);
        }

        /// <summary>
        /// GET api/solicitudes/paciente/{pacienteId} - Obtener solicitudes de un paciente
        /// </summary>
        [HttpGet("paciente/{pacienteId:long}")]
        public IActionResult GetByPaciente(long pacienteId)
        {
            var solicitudes = _db.SolicitudesExamen
                .Where(s => s.IdPaciente == pacienteId)
                .Select(s => new SolicitudDto
                {
                    IdSolicitud = s.IdSolicitud,
                    NroOrden = s.NroOrden,
                    IdPaciente = s.IdPaciente,
                    FechaSolicitud = s.FechaSolicitud,
                    IsAnulado = s.IsAnulado,
                    CantidadExamenes = s.Examenes != null ? s.Examenes.Count : 0
                })
                .ToList();

            return Ok(solicitudes);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSolicitudCommand command)
        {
            var id = await _dispatcher.Send<CreateSolicitudCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
