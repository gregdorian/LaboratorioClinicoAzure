using System;

namespace Lab.Api.Application.DTOs
{
    public class CitaDetailDto
    {
        public long IdCita { get; set; }
        public long IdPaciente { get; set; }
        public string? NombrePaciente { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Estado { get; set; } // "Programada", "Confirmada", "Cancelada"
        public string? Motivo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? ObservacionesPaciente { get; set; }
    }
}
