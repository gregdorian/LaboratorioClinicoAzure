using System;

namespace Lab.Api.Domain.Entities
{
    public class FacturaElectronica
    {
        public long IdFacturaElectronica { get; set; }
        public long IdFactura { get; set; }
        public string CUFE { get; set; } = null!;
        public string NroFacturaDIAN { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public string EstadoDIAN { get; set; } = "Emitida";
    }
}
