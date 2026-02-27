using System;

namespace Lab.Api.Domain.Entities
{
    public class Persona
    {
        public long IdPersona { get; set; }
        public string IdTipoDocumento { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string? IdSexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public long? IdDireccion { get; set; }
    }
}
