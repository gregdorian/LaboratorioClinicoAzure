using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.DTOs;
using System.Threading.Tasks;

namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public CitasController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpPost("programar")]
        public async Task<IActionResult> Programar(ProgramarCitaCommand command)
        {
            var id = await _dispatcher.Send<ProgramarCitaCommand, long>(command);
            return CreatedAtAction(nameof(Programar), new { id }, new { Id = id });
        }
    }
}
