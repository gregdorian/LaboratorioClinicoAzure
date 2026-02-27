using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Domain.Entities;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Commands
{
    public class CreatePacienteHandler : ICommandHandler<CreatePacienteCommand, long>
    {
        private readonly LabDbContext _db;
        public CreatePacienteHandler(LabDbContext db) => _db = db;

        public async Task<long> Handle(CreatePacienteCommand command)
        {
            var persona = new Persona
            {
                IdTipoDocumento = command.IdTipoDocumento,
                NroIdentificacion = command.NroIdentificacion,
                Nombre = command.Nombre,
                PrimerApellido = command.PrimerApellido,
                SegundoApellido = command.SegundoApellido,
                IdSexo = command.IdSexo,
                FechaNacimiento = command.FechaNacimiento
            };
            _db.Persona.Add(persona);
            await _db.SaveChangesAsync();

            var paciente = new Paciente
            {
                IdPersona = persona.IdPersona,
                NroHistoriaClinica = command.NroHistoriaClinica,
                TipoSangre = command.TipoSangre
            };
            _db.Paciente.Add(paciente);
            await _db.SaveChangesAsync();

            return paciente.IdPaciente;
        }
    }
}
