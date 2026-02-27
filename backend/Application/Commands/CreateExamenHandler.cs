using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Commands
{
    public class CreateExamenHandler : ICommandHandler<CreateExamenCommand, long>
    {
        private readonly LabDbContext _db;
        public CreateExamenHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(CreateExamenCommand command)
        {
            var e = new Examen
            {
                IdArea = command.IdArea,
                CodigoExamen = command.CodigoExamen,
                NombreExamen = command.NombreExamen,
                IdCUPS = command.IdCUPS,
                PrecioBase = command.PrecioBase,
                TiempoEntregaHoras = command.TiempoEntregaHoras
            };
            _db.Examenes.Add(e);
            await _db.SaveChangesAsync();
            return e.IdExamen;
        }
    }
}
