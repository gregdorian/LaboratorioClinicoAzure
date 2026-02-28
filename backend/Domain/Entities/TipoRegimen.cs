namespace Lab.Api.Domain.Entities
{
    public class TipoRegimen
    {
        public string IdTipoRegimen { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Notas { get; set; }
    }
}
