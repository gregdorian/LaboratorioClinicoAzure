using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lab.Api.Application.Commands
{
    public class ProgramarCitaHandler : ICommandHandler<ProgramarCitaCommand, long>
    {
        private readonly LabDbContext _db;
        public ProgramarCitaHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(ProgramarCitaCommand command)
        {
            // Call stored procedure sp_ProgramarCita to handle concurrency and notifications in DB
            var idCitaParam = new Microsoft.Data.SqlClient.SqlParameter("@IdCitaNueva", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output };
            var mensajeParam = new Microsoft.Data.SqlClient.SqlParameter("@Mensaje", System.Data.SqlDbType.NVarChar, 200) { Direction = System.Data.ParameterDirection.Output };
            var rowVerParam = command.RowVerEsperado != null
                ? new Microsoft.Data.SqlClient.SqlParameter("@RowVerEsperado", System.Data.SqlDbType.Binary, 8) { Value = command.RowVerEsperado }
                : new Microsoft.Data.SqlClient.SqlParameter("@RowVerEsperado", System.Data.SqlDbType.Binary, 8) { Value = System.DBNull.Value };

            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_ProgramarCita @IdPaciente, @IdDisponibilidad, @RowVerEsperado, @Motivo, @HistorialMedico, @Medicamentos, @Observaciones, @IdCitaNueva OUTPUT, @Mensaje OUTPUT",
                new Microsoft.Data.SqlClient.SqlParameter("@IdPaciente", command.IdPaciente),
                new Microsoft.Data.SqlClient.SqlParameter("@IdDisponibilidad", command.IdDisponibilidad),
                rowVerParam,
                new Microsoft.Data.SqlClient.SqlParameter("@Motivo", command.Motivo ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@HistorialMedico", command.HistorialMedico ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@Medicamentos", command.Medicamentos ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@Observaciones", command.Observaciones ?? (object)System.DBNull.Value),
                idCitaParam,
                mensajeParam
            );

            var id = (idCitaParam.Value == System.DBNull.Value) ? -1L : Convert.ToInt64(idCitaParam.Value);
            if (id <= 0) throw new System.Exception((mensajeParam.Value as string) ?? "Error scheduling");
            return id;
        }
    }
}
