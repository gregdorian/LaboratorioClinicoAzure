namespace Lab.Api.Application.DTOs
{
    public class PacienteDto
    {
        public long IdPaciente { get; set; }
        public string? NroHistoriaClinica { get; set; }
        public string? NroIdentificacion { get; set; }
        public string? NombreCompleto { get; set; }
    }
}
