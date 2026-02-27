using System;

namespace Lab.Api.Domain.Entities
{
    public class Cita
    {
        public long IdCita { get; set; }
        public long IdPaciente { get; set; }
        public long IdDisponibilidad { get; set; }
        public byte IdEstadoCita { get; set; } = 1;
        public long? IdSolicitud { get; set; }
        public string? Motivo { get; set; }
        public string? HistorialMedico { get; set; }
        public string? Medicamentos { get; set; }
        public string? ObservacionesPac { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaConfirmacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
    }
}
