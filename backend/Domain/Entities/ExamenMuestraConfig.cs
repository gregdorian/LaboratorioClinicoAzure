namespace Lab.Api.Domain.Entities
{
    public class ExamenMuestraConfig
    {
        public long IdExamenMuestraConfig { get; set; }
        public long IdExamen { get; set; }
        public long IdTipoMuestra { get; set; }
        public decimal? VolumenML { get; set; }
        public string? Instrucciones { get; set; }
    }
}
