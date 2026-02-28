using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Infrastructure;
using System.Threading.Tasks;
using System.Linq;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RipsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly LabDbContext _db;
        public RipsController(IDispatcher dispatcher, LabDbContext db) { _dispatcher = dispatcher; _db = db; }

        [HttpPost("generar")]
        public async Task<IActionResult> Generar([FromBody] GenerarRipsCommand cmd)
        {
            var created = await _dispatcher.Send<GenerarRipsCommand, int>(cmd);
            return Ok(new { Created = created });
        }

        [HttpPost("generar-lote")]
        public async Task<IActionResult> GenerarLote([FromBody] GenerarLoteRipsCommand cmd)
        {
            var id = await _dispatcher.Send<GenerarLoteRipsCommand, long>(cmd);
            return Ok(new { Id = id });
        }

        [HttpGet("export")]
        public IActionResult Export(string numeroLote)
        {
            var rows = _db.RipsTransaccional.Where(r => r.LoteRips == numeroLote).ToList();
            if (!rows.Any()) return NotFound();

            var csv = "ConsecutivoRIPS,FechaAtencion,CodigoCUPS,ValorTotal,IdSolicitud\n" + string.Join('\n', rows.Select(r => $"{r.ConsecutivoRips},{r.FechaAtencion:O},{r.CodigoCups},{r.ValorTotal},{r.IdSolicitud}"));
            return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", numeroLote + ".csv");
        }
    }
}
