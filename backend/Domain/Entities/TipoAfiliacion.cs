namespace Lab.Api.Domain.Entities
{
    public class TipoAfiliacion
    {
        public long IdTipoAfiliacion { get; set; }
        public long IdPrestadora { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
