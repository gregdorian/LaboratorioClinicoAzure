using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
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
            var fechaSlot = command.FechaInicio;
            var publicados = 0;

            for (int i = 0; i < command.CantidadSlots; i++)
            {
                var exists = await _db.DisponibilidadHoraria
                    .FindAsync(new object[] { 0 });

                // simple existence check by FechaHora + CodigoSede
                var conflict = await _db.DisponibilidadHoraria
                    .FirstOrDefaultAsync(d => d.CodigoSede == command.CodigoSede && d.FechaHora == fechaSlot && d.Activo);

                if (conflict == null)
                {
                    var d = new DisponibilidadHoraria
                    {
                        CodigoSede = command.CodigoSede,
                        IdPersonalLaboratorio = command.IdPersonalLaboratorio,
                        FechaHora = fechaSlot,
                        DuracionMinutos = command.DuracionMinutos,
                        CupoMaximo = command.CupoMaximoPorSlot,
                        CuposOcupados = 0,
                        Activo = true
                    };
                    _db.DisponibilidadHoraria.Add(d);
                    publicados++;
                }

                fechaSlot = fechaSlot.AddMinutes(command.DuracionMinutos);
            }

            await _db.SaveChangesAsync();
            return publicados;
        }
    }
}
