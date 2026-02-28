using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Microsoft.EntityFrameworkCore;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;
using System;

namespace Lab.Api.Application.Commands
{
    public class PublishSlotsHandler : ICommandHandler<PublishSlotsCommand, int>
    {
        private readonly LabDbContext _db;
        public PublishSlotsHandler(LabDbContext db) => _db = db;

        public async Task<int> Handle(PublishSlotsCommand command)
        {
            // Use stored procedure sp_PublicarSlots to publish slots in DB
            var mensajeParam = new Microsoft.Data.SqlClient.SqlParameter("@Mensaje", System.Data.SqlDbType.NVarChar, 200) { Direction = System.Data.ParameterDirection.Output };
            await _db.Database.ExecuteSqlRawAsync(
                "EXEC sp_PublicarSlots @CodigoSede, @IdPersonalLaboratorio, @FechaInicio, @CantidadSlots, @DuracionMinutos, @CupoMaximoPorSlot, @Mensaje OUTPUT",
                new Microsoft.Data.SqlClient.SqlParameter("@CodigoSede", command.CodigoSede ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@IdPersonalLaboratorio", command.IdPersonalLaboratorio ?? (object)System.DBNull.Value),
                new Microsoft.Data.SqlClient.SqlParameter("@FechaInicio", command.FechaInicio),
                new Microsoft.Data.SqlClient.SqlParameter("@CantidadSlots", command.CantidadSlots),
                new Microsoft.Data.SqlClient.SqlParameter("@DuracionMinutos", command.DuracionMinutos),
                new Microsoft.Data.SqlClient.SqlParameter("@CupoMaximoPorSlot", command.CupoMaximoPorSlot),
                mensajeParam
            );

            var mensaje = (mensajeParam.Value as string) ?? string.Empty;
            // try to parse number from message, fallback to requested count
            if (mensaje.StartsWith("OK:")) return command.CantidadSlots;
            return 0;
        }
    }
}
