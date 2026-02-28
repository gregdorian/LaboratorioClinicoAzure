using System;

namespace Lab.Api.Domain.Entities
{
    public class EntidadPagadora
    {
        public long IdEntidadPagadora { get; set; }
        public string TipoEntidad { get; set; } = null!;
        public string CodigoEntidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? NIT { get; set; }
        public long? IdDireccion { get; set; }
    }
}
