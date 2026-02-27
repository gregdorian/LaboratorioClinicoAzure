using System;

namespace Lab.Api.Domain.Entities
{
    public class Factura
    {
        public long IdFactura { get; set; }
        public string NroFactura { get; set; } = null!;
        public long IdSolicitud { get; set; }
        public long IdEntidadPagadora { get; set; }
        public decimal MontoTotal { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
        public DateTime FechaFacturacion { get; set; }
    }
}
