using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.Commands;
using System.Threading.Tasks;

namespace Lab.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public NotificationsController(IDispatcher dispatcher) => _dispatcher = dispatcher;

        [HttpPost("enqueue")]
        public async Task<IActionResult> Enqueue(EnqueueNotificationCommand command)
        {
            var id = await _dispatcher.Send<EnqueueNotificationCommand, long>(command);
            return CreatedAtAction(nameof(Enqueue), new { id }, new { Id = id });
        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromQuery]int max = 50)
        {
            var processed = await _dispatcher.Send<ProcessPendingNotificationsCommand, int>(new ProcessPendingNotificationsCommand { MaxItems = max });
            return Ok(new { Processed = processed });
        }
    }
}
