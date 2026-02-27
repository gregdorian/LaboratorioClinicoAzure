using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using Lab.Api.Application.Queries;
using Lab.Api.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamenesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public ExamenesController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IEnumerable<ExamenDto>> Get() => await _dispatcher.Query<GetExamenesQuery, IEnumerable<ExamenDto>>(new GetExamenesQuery());

        [HttpPost]
        public async Task<IActionResult> Post(CreateExamenCommand command)
        {
            var id = await _dispatcher.Send<CreateExamenCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
