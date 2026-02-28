using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.Queries;
using Lab.Api.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PacientesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public PacientesController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IEnumerable<PacienteDto>> Get()
        {
            return await _dispatcher.Query<GetPacientesQuery, IEnumerable<PacienteDto>>(new GetPacientesQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePacienteCommand command)
        {
            var id = await _dispatcher.Send<CreatePacienteCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
