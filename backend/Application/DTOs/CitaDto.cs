using System;

namespace Lab.Api.Application.DTOs
{
    public class CitaDto
    {
        public long IdCita { get; set; }
        public long IdPaciente { get; set; }
        public long IdDisponibilidad { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
