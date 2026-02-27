using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Queries
{
    public class GetExamenesHandler : IQueryHandler<GetExamenesQuery, IEnumerable<ExamenDto>>
    {
        private readonly LabDbContext _db;
        public GetExamenesHandler(LabDbContext db) => _db = db;

        public Task<IEnumerable<ExamenDto>> Handle(GetExamenesQuery query)
        {
            var list = _db.Examenes.Select(e => new ExamenDto
            {
                IdExamen = e.IdExamen,
                CodigoExamen = e.CodigoExamen,
                NombreExamen = e.NombreExamen
            }).ToList().AsEnumerable();

            return Task.FromResult(list);
        }
    }
}
