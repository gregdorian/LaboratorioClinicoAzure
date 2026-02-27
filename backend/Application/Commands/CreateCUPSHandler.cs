using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Commands
{
    public class CreateCUPSHandler : ICommandHandler<CreateCUPSCommand, long>
    {
        private readonly LabDbContext _db;
        public CreateCUPSHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(CreateCUPSCommand command)
        {
            var c = new CUPS
            {
                CodigoCUPS = command.CodigoCUPS,
                Descripcion = command.Descripcion,
                TipoServicio = command.TipoServicio,
                UnidadMedida = command.UnidadMedida,
                NivelAtencion = command.NivelAtencion
            };
            _db.CUPS.Add(c);
            await _db.SaveChangesAsync();
            return c.IdCUPS;
        }
    }
}
