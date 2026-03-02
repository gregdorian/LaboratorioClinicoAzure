using System;

namespace Lab.Api.Application.DTOs
{
    public class MuestraDto
    {
        public long IdMuestra { get; set; }
        public long IdSolicitud { get; set; }
        public long IdPaciente { get; set; }
        public string? TipoMuestra { get; set; }
        public string? AreaAnalisis { get; set; }
        public string? Prioridad { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string? Estado { get; set; }
    }

    public class RecepcionMuestraDto
    {
        public long IdSolicitud { get; set; }
        public long IdPaciente { get; set; }
        public string? TipoMuestra { get; set; }
    }

    public class AnalisisMuestraDto
    {
        public string? Resultado { get; set; }
        public string? Observaciones { get; set; }
    }
}
