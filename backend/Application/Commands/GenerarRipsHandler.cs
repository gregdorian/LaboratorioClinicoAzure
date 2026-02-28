using System;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Infrastructure;
using Lab.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab.Api.Application.Commands
{
    public class GenerarRipsHandler : ICommandHandler<GenerarRipsCommand, int>
    {
        private readonly LabDbContext _db;
        public GenerarRipsHandler(LabDbContext db) => _db = db;

        public async Task<int> Handle(GenerarRipsCommand command)
        {
            var solicitudes = await _db.SolicitudesExamen
                .Where(s => s.FechaSolicitud.Date >= command.FechaInicio.Date && s.FechaSolicitud.Date <= command.FechaFin.Date && !s.IsAnulado)
                .ToListAsync();

            var created = 0;
            foreach (var s in solicitudes)
            {
                // generate a simple consecutivo: YYYYMMDD + Guid short
                var cons = s.FechaSolicitud.ToString("yyyyMM") + DateTime.Now.ToString("dd") + "-" + Guid.NewGuid().ToString("N").Substring(0,6);

                var r = new RipsTransaccional
                {
                    IdSolicitud = s.IdSolicitud,
                    IdFactura = null,
                    ConsecutivoRips = cons,
                    CodigoCups = "000000",
                    FechaAtencion = s.FechaSolicitud,
                    ValorTotal = 0,
                    EsGenerado = true,
                    LoteRips = null
                };

                _db.RipsTransaccional.Add(r);
                created++;
            }

            await _db.SaveChangesAsync();
            return created;
        }
    }
}
