using System;

namespace Lab.Api.Application.DTOs
{
    public class SolicitudDto
    {
        public long IdSolicitud { get; set; }
        public string? NroOrden { get; set; }
        public long IdPaciente { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool IsAnulado { get; set; }
        public int CantidadExamenes { get; set; }
    }
}
