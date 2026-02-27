using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Commands
{
    public class CreateFacturaHandler : ICommandHandler<CreateFacturaCommand, long>
    {
        private readonly LabDbContext _db;
        public CreateFacturaHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(CreateFacturaCommand command)
        {
            var factura = new Factura
            {
                IdSolicitud = command.IdSolicitud,
                IdEntidadPagadora = command.IdEntidadPagadora,
                MontoTotal = command.MontoTotal,
                FechaFacturacion = command.FechaFacturacion,
                NroFactura = "FAC-API-" + System.Guid.NewGuid().ToString("N").Substring(0,8)
            };

            _db.Factura.Add(factura);
            await _db.SaveChangesAsync();
            return factura.IdFactura;
        }
    }
}
