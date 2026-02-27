using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using System.Threading.Tasks;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public FacturasController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpPost]
        public async Task<IActionResult> Post(CreateFacturaCommand command)
        {
            var id = await _dispatcher.Send<CreateFacturaCommand, long>(command);
            return CreatedAtAction(nameof(Post), new { id }, new { Id = id });
        }
    }
}
