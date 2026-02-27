using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Queries
{
    public class GetPacientesHandler : IQueryHandler<GetPacientesQuery, IEnumerable<PacienteDto>>
    {
        private readonly LabDbContext _db;
        public GetPacientesHandler(LabDbContext db) => _db = db;

        public Task<IEnumerable<PacienteDto>> Handle(GetPacientesQuery query)
        {
            var list = _db.Paciente
                .Select(p => new PacienteDto
                {
                    IdPaciente = p.IdPaciente,
                    NroHistoriaClinica = p.NroHistoriaClinica,
                    NroIdentificacion = p.Persona != null ? p.Persona.NroIdentificacion : string.Empty,
                    NombreCompleto = p.Persona != null ? (p.Persona.Nombre + " " + p.Persona.PrimerApellido) : string.Empty
                })
                .ToList()
                .AsEnumerable();

            return Task.FromResult(list);
        }
    }
}
