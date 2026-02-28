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
    public class SolicitudesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public SolicitudesController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpPost]
        public async Task<IActionResult> Post(CreateSolicitudCommand command)
        {
            var id = await _dispatcher.Send<CreateSolicitudCommand, long>(command);
            return CreatedAtAction(nameof(Post), new { id }, new { Id = id });
        }
    }
}
