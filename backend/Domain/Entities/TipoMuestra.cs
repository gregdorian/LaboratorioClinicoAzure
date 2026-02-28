namespace Lab.Api.Domain.Entities
{
    public class TipoMuestra
    {
        public long IdTipoMuestra { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
