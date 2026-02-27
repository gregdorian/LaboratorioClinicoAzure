using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Api.Application.CQRS;
using Lab.Api.Application.DTOs;
using Lab.Api.Infrastructure;

namespace Lab.Api.Application.Queries
{
    public class GetCUPSHandler : IQueryHandler<GetCUPSQuery, IEnumerable<CUPSDto>>
    {
        private readonly LabDbContext _db;
        public GetCUPSHandler(LabDbContext db) => _db = db;

        public Task<IEnumerable<CUPSDto>> Handle(GetCUPSQuery query)
        {
            var list = _db.CUPS.Select(c => new CUPSDto
            {
                IdCUPS = c.IdCUPS,
                CodigoCUPS = c.CodigoCUPS,
                Descripcion = c.Descripcion,
                TipoServicio = c.TipoServicio
            }).ToList().AsEnumerable();

            return Task.FromResult(list);
        }
    }
}
