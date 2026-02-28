using System;

namespace Lab.Api.Domain.Entities
{
    public class AuditLog
    {
        public long IdAudit { get; set; }
        public string Tabla { get; set; } = null!;
        public long IdRegistro { get; set; }
        public char Operacion { get; set; }
        public string Usuario { get; set; } = null!;
        public DateTime FechaHora { get; set; }
        public string? Antes { get; set; }
        public string? Despues { get; set; }
    }
}
