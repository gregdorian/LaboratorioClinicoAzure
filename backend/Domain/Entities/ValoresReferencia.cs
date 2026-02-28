namespace Lab.Api.Domain.Entities
{
    public class ValoresReferencia
    {
        public long IdValorReferencia { get; set; }
        public long IdPrueba { get; set; }
        public string IdSexo { get; set; } = "A";
        public long IdRangoEdad { get; set; }
        public decimal? LimiteInferior { get; set; }
        public decimal? LimiteSuperior { get; set; }
        public string? Unidad { get; set; }
    }
}
