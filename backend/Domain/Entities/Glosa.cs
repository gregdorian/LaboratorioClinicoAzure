using System;

namespace Lab.Api.Domain.Entities
{
    public class Glosa
    {
        public long IdGlosa { get; set; }
        public long IdFactura { get; set; }
        public long? IdFacturaDetalle { get; set; }
        public string CodigoGlosa { get; set; } = null!;
        public string DescripcionGlosa { get; set; } = null!;
        public decimal ValorGlosado { get; set; }
        public DateTime FechaGlosa { get; set; }
        public string EstadoGlosa { get; set; } = "Pendiente";
    }
}
