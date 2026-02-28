using System;

namespace Lab.Api.Domain.Entities
{
    public class Contrato
    {
        public long IdContrato { get; set; }
        public long IdEntidadPagadora { get; set; }
        public string NumeroContrato { get; set; } = null!;
        public string TipoContrato { get; set; } = "Tarifado";
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
