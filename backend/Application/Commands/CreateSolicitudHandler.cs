using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;
using System.Linq;

namespace Lab.Api.Application.Commands
{
    public class CreateSolicitudHandler : ICommandHandler<CreateSolicitudCommand, long>
    {
        private readonly LabDbContext _db;
        public CreateSolicitudHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(CreateSolicitudCommand command)
        {
            var solicitud = new SolicitudExamen
            {
                IdPaciente = command.IdPaciente,
                IdMedico = command.IdMedico,
                FechaSolicitud = command.FechaSolicitud,
                NroOrden = "LAB-API-" + System.Guid.NewGuid().ToString("N").Substring(0,8)
            };
            _db.SolicitudesExamen.Add(solicitud);
            await _db.SaveChangesAsync();

            var items = command.Examenes.Select(x => new ExamenSolicitado
            {
                IdSolicitud = solicitud.IdSolicitud,
                IdExamen = x.IdExamen,
                Cantidad = x.Cantidad,
                ValorUnitario = x.ValorUnitario,
                ValorTotal = x.ValorUnitario * x.Cantidad
            }).ToList();

            _db.ExamenesSolicitados.AddRange(items);
            await _db.SaveChangesAsync();

            return solicitud.IdSolicitud;
        }
    }
}
