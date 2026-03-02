namespace Lab.Api.Domain.Entities
{
    public class Paciente
    {
        public long IdPaciente { get; set; }
        public long IdPersona { get; set; }
        public string? NroHistoriaClinica { get; set; }
        public string? TipoSangre { get; set; }

        // Propiedades de navegación
        public Persona? Persona { get; set; }
    }
}
