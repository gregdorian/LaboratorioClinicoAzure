using System;

namespace Lab.Api.Application.DTOs
{
    public class ResultadoSearchDto
    {
        public long IdResultado { get; set; }
        public string? NombrePaciente { get; set; }
        public string? ApellidoPaciente { get; set; }
        public string? NroIdentificacion { get; set; }
        public DateTime FechaAnalisis { get; set; }
        public string? Resultado { get; set; }
        public string? Observaciones { get; set; }
    }

    public class ResultadoDetailDto
    {
        public long IdResultado { get; set; }
        public long IdMuestra { get; set; }
        public string? TipoMuestra { get; set; }
        public string? Resultado { get; set; }
        public string? Unidad { get; set; }
        public string? RangoReferencia { get; set; }
        public string? Estado { get; set; } // "Normal", "Anormal", "Pendiente"
        public DateTime FechaAnalisis { get; set; }
        public string? Observaciones { get; set; }
    }
}
