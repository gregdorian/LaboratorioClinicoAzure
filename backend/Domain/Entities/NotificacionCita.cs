using System;

namespace Lab.Api.Domain.Entities
{
    public class NotificacionCita
    {
        public long IdNotificacion { get; set; }
        public long IdPaciente { get; set; }
        public long? IdCita { get; set; }
        public long? IdResultado { get; set; }
        public string TipoNotif { get; set; } = null!;
        public string Destinatario { get; set; } = null!;
        public string Asunto { get; set; } = null!;
        public string? Cuerpo { get; set; }
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
        public bool Enviado { get; set; } = false;
        public string? MensajeError { get; set; }
        public int IntentosEnvio { get; set; } = 0;
        public DateTime ProximoIntento { get; set; } = DateTime.UtcNow;
        public int MaxIntentos { get; set; } = 5;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
        public bool Eliminado { get; set; } = false;
    }
}
