using System;

namespace Lab.Api.Application.Commands
{
    public class ProgramarCitaCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public long IdPaciente { get; set; }
        public long IdDisponibilidad { get; set; }
        public byte[]? RowVerEsperado { get; set; }
        public string? Motivo { get; set; }
        public string? HistorialMedico { get; set; }
        public string? Medicamentos { get; set; }
        public string? Observaciones { get; set; }
    }
}
