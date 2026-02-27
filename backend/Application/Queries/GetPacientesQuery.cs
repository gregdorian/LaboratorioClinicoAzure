using Lab.Api.Application.DTOs;

namespace Lab.Api.Application.Queries
{
    public class GetPacientesQuery : Lab.Api.Application.CQRS.IQuery<System.Collections.Generic.IEnumerable<PacienteDto>>
    {
    }
}
