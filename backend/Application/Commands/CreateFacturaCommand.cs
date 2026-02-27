using System;

namespace Lab.Api.Application.Commands
{
    public class CreateFacturaCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public long IdSolicitud { get; set; }
        public long IdEntidadPagadora { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaFacturacion { get; set; } = DateTime.UtcNow;
    }
}
