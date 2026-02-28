using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;
using System.Threading.Tasks;
using System.Linq;

namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DisponibilidadController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly LabDbContext _db;
        public DisponibilidadController(IDispatcher dispatcher, LabDbContext db) { _dispatcher = dispatcher; _db = db; }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish(PublishSlotsCommand command)
        {
            var created = await _dispatcher.Send<PublishSlotsCommand, int>(command);
            return Ok(new { Created = created });
        }

        [HttpGet("slots")]
        public IActionResult GetSlots(string codigoSede, System.DateTime fecha)
        {
            var res = _db.DisponibilidadHoraria
                .Where(d => d.CodigoSede == codigoSede && d.FechaHora.Date == fecha.Date && d.CuposOcupados < d.CupoMaximo && d.Activo)
                .Select(d => new DisponibilidadDto {
                    IdDisponibilidad = d.IdDisponibilidad,
                    CodigoSede = d.CodigoSede,
                    FechaHora = d.FechaHora,
                    DuracionMinutos = d.DuracionMinutos,
                    CupoMaximo = d.CupoMaximo,
                    CuposOcupados = d.CuposOcupados,
                    RowVer = d.RowVer
                }).ToList();

            return Ok(res);
        }
    }
}
