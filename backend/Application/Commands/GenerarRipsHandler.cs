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
            // Use stored procedure sp_GenerarRIPS_Transaccional to generate RIPS in DB
            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_GenerarRIPS_Transaccional @FechaInicio, @FechaFin, @IdEntidad, @Usuario",
                new Microsoft.Data.SqlClient.SqlParameter("@FechaInicio", command.FechaInicio),
                new Microsoft.Data.SqlClient.SqlParameter("@FechaFin", command.FechaFin),
                new Microsoft.Data.SqlClient.SqlParameter("@IdEntidad", command.IdEntidad ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@Usuario", command.Usuario ?? (object)System.DBNull.Value)
            );

            // count generated rows in range
            var count = await _db.RipsTransaccional.CountAsync(r => r.FechaAtencion.Date >= command.FechaInicio.Date && r.FechaAtencion.Date <= command.FechaFin.Date && r.EsGenerado);
            return count;
        }
    }
}
