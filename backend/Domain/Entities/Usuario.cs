using System;

namespace Lab.Api.Domain.Entities
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public long IdPersona { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string ClaveHash { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;
        public string Estado { get; set; } = "Activo";
        public DateTime? UltimoAcceso { get; set; }
    }
}
