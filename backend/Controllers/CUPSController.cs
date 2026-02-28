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
    public class CUPSController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public CUPSController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IEnumerable<CUPSDto>> Get()
        {
            return await _dispatcher.Query<GetCUPSQuery, IEnumerable<CUPSDto>>(new GetCUPSQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCUPSCommand command)
        {
            var id = await _dispatcher.Send<CreateCUPSCommand, long>(command);
            return CreatedAtAction(nameof(Get), new { id }, new { Id = id });
        }
    }
}
