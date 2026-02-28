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
            // Call stored procedure sp_GenerarLoteRIPS which returns @IdLote and @Mensaje
            var idLoteParam = new Microsoft.Data.SqlClient.SqlParameter("@IdLote", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output };
            var mensajeParam = new Microsoft.Data.SqlClient.SqlParameter("@Mensaje", System.Data.SqlDbType.NVarChar, 500) { Direction = System.Data.ParameterDirection.Output };

            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_GenerarLoteRIPS @FechaInicio, @FechaFin, @Usuario, @IdLote OUTPUT, @Mensaje OUTPUT",
                new Microsoft.Data.SqlClient.SqlParameter("@FechaInicio", command.FechaInicio),
                new Microsoft.Data.SqlClient.SqlParameter("@FechaFin", command.FechaFin),
                new Microsoft.Data.SqlClient.SqlParameter("@Usuario", command.Usuario ?? (object)System.DBNull.Value),
                idLoteParam,
                mensajeParam
            );

            var id = (idLoteParam.Value == System.DBNull.Value) ? -1L : Convert.ToInt64(idLoteParam.Value);
            if (id <= 0) throw new System.Exception((mensajeParam.Value as string) ?? "Error creating lote RIPS");
            return id;
        }
    }
}
