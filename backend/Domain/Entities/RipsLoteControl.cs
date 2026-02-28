using System;

namespace Lab.Api.Domain.Entities
{
    public class RipsLoteControl
    {
        public long IdLote { get; set; }
        public string NumeroLote { get; set; } = null!;
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaEnvio { get; set; }
        public int CantidadRegistros { get; set; } = 0;
        public string EstadoEnvio { get; set; } = "Pendiente";
        public bool Eliminado { get; set; } = false;
        public string? UsuarioGeneracion { get; set; }
    }
}
