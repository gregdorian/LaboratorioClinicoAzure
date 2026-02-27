using System;
using System.Collections.Generic;

namespace Lab.Api.Domain.Entities
{
    public class SolicitudExamen
    {
        public long IdSolicitud { get; set; }
        public string NroOrden { get; set; } = null!;
        public long IdPaciente { get; set; }
        public long? IdMedico { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool IsAnulado { get; set; }

        public List<ExamenSolicitado>? Examenes { get; set; }
    }
}
