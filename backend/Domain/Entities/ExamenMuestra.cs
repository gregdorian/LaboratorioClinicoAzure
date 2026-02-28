using System;

namespace Lab.Api.Domain.Entities
{
    public class ExamenMuestra
    {
        public long IdExamenMuestra { get; set; }
        public long IdExamenSolicitado { get; set; }
        public long IdExamen { get; set; }
        public long IdTipoMuestra { get; set; }
        public long IdPersonalLaboratorio { get; set; }
        public DateTime FechaToma { get; set; }
    }
}
