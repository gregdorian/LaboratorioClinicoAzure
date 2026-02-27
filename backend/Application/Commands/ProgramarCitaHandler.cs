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
            using var tx = await _db.Database.BeginTransactionAsync();

            var slot = await _db.DisponibilidadHoraria
                .Where(d => d.IdDisponibilidad == command.IdDisponibilidad && d.Activo)
                .FirstOrDefaultAsync();

            if (slot == null) throw new System.Exception("Slot not found or inactive");

            if (command.RowVerEsperado != null && !slot.RowVer.SequenceEqual(command.RowVerEsperado))
                throw new System.Exception("El slot fue modificado concurrentemente.");

            if (slot.CuposOcupados >= slot.CupoMaximo) throw new System.Exception("No hay cupo disponible.");

            // check patient doesn't already have an active cita for same slot
            var exists = await _db.Citas.AnyAsync(c => c.IdPaciente == command.IdPaciente && c.IdDisponibilidad == command.IdDisponibilidad && c.IdEstadoCita != 3);
            if (exists) throw new System.Exception("El paciente ya tiene una cita en ese horario.");

            var cita = new Cita
            {
                IdPaciente = command.IdPaciente,
                IdDisponibilidad = command.IdDisponibilidad,
                Motivo = command.Motivo,
                HistorialMedico = command.HistorialMedico,
                Medicamentos = command.Medicamentos,
                ObservacionesPac = command.Observaciones,
                FechaRegistro = System.DateTime.UtcNow
            };

            _db.Citas.Add(cita);

            // increment cupos
            slot.CuposOcupados += 1;
            _db.DisponibilidadHoraria.Update(slot);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return cita.IdCita;
        }
    }
}
