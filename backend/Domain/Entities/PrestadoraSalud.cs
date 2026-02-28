namespace Lab.Api.Domain.Entities
{
    public class PrestadoraSalud
    {
        public long IdPrestadora { get; set; }
        public string IdTipoRegimen { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? NIT { get; set; }
        public long? IdDireccion { get; set; }
    }
}
