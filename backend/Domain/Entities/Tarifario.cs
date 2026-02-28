using System;

namespace Lab.Api.Domain.Entities
{
    public class Tarifario
    {
        public long IdTarifario { get; set; }
        public long IdContrato { get; set; }
        public long IdCUPS { get; set; }
        public long? IdExamen { get; set; }
        public decimal ValorUnitario { get; set; }
        public DateTime FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
    }
}
