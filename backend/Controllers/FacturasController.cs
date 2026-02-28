using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Infrastructure;
using System.Threading.Tasks;
using System.Linq;


namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Facturador")]
    public class FacturasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly LabDbContext _db;
        public FacturasController(IDispatcher dispatcher, LabDbContext db) { _dispatcher = dispatcher; _db = db; }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var items = _db.Factura.Select(f => new { f.IdFactura, f.NroFactura, f.MontoTotal, f.EstadoPago, f.FechaFacturacion }).ToList();
            return Ok(items);
        }

        [HttpGet("{id:long}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var f = _db.Factura.Where(x => x.IdFactura == id).Select(x => new { x.IdFactura, x.NroFactura, x.MontoTotal, x.EstadoPago, x.FechaFacturacion, x.IdSolicitud, x.IdEntidadPagadora }).FirstOrDefault();
            if (f == null) return NotFound();
            return Ok(f);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateFacturaCommand command)
        {
            var id = await _dispatcher.Send<CreateFacturaCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
