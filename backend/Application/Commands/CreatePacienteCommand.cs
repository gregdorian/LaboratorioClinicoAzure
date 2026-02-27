using Lab.Api.Application.DTOs;

namespace Lab.Api.Application.Commands
{
    public class CreatePacienteCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public string IdTipoDocumento { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string? IdSexo { get; set; }
        public System.DateTime? FechaNacimiento { get; set; }
        public string? NroHistoriaClinica { get; set; }
        public string? TipoSangre { get; set; }
    }
}
