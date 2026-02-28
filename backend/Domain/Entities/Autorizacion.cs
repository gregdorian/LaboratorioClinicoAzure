using System;

namespace Lab.Api.Domain.Entities
{
    public class Autorizacion
    {
        public long IdAutorizacion { get; set; }
        public string NumeroAutorizacion { get; set; } = null!;
        public long IdEntidadPagadora { get; set; }
        public long IdPaciente { get; set; }
        public DateTime FechaAutorizacion { get; set; }
    }
}
