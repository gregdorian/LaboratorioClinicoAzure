using System;

namespace Lab.Api.Application.Commands
{
    public class EnqueueNotificationCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public long IdPaciente { get; set; }
        public long? IdCita { get; set; }
        public string TipoNotif { get; set; } = null!;
        public string Destinatario { get; set; } = null!;
        public string Asunto { get; set; } = null!;
        public string? Cuerpo { get; set; }
    }
}
