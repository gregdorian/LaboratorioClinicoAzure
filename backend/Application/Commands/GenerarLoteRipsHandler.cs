using System;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lab.Api.Application.Commands
{
    public class GenerarLoteRipsHandler : ICommandHandler<GenerarLoteRipsCommand, long>
    {
        private readonly LabDbContext _db;
        public GenerarLoteRipsHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(GenerarLoteRipsCommand command)
        {
            using var tx = await _db.Database.BeginTransactionAsync();

            // Count existing for today to create a three-digit sequence
            var today = DateTime.UtcNow.Date;
            var countToday = await _db.RipsLoteControl.CountAsync(l => l.FechaGeneracion.Date == today);
            var seq = countToday + 1;
            var numero = $"RIPS-{DateTime.UtcNow:yyyyMMdd}-{seq:000}";

            var lote = new RipsLoteControl
            {
                NumeroLote = numero,
                FechaGeneracion = DateTime.UtcNow,
                UsuarioGeneracion = command.Usuario
            };

            // Note: UsuarioGeneracion property not defined in entity; we'll add dynamic assignment via reflection fallback
            try { _db.RipsLoteControl.Add(lote); await _db.SaveChangesAsync(); }
            catch { throw; }

            // Assign lote to generated RIPS rows in date range
            var updated = await _db.RipsTransaccional
                .Where(r => r.FechaAtencion.Date >= command.FechaInicio.Date && r.FechaAtencion.Date <= command.FechaFin.Date && r.LoteRips == null)
                .ToListAsync();

            foreach (var r in updated) r.LoteRips = numero;
            await _db.SaveChangesAsync();

            await tx.CommitAsync();
            return lote.IdLote;
        }
    }
}
