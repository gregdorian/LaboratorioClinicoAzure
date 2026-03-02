using System.Collections.Generic;

namespace Lab.Api.Application.DTOs
{
    public class LaboratorioDto
    {
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Ciudad { get; set; }
        public string? Horario { get; set; }
        public List<string>? Examenes { get; set; }
    }
}
