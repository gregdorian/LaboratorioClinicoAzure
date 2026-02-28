using System;

namespace Lab.Api.Domain.Entities
{
    public class RipsTransaccional
    {
        public long IdRips { get; set; }
        public long IdSolicitud { get; set; }
        public long? IdFactura { get; set; }
        public string ConsecutivoRips { get; set; } = null!;
        public string CodigoCups { get; set; } = null!;
        public DateTime FechaAtencion { get; set; }
        public decimal ValorTotal { get; set; }
        public bool EsGenerado { get; set; }
        public string? LoteRips { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool Eliminado { get; set; } = false;
    }
}
