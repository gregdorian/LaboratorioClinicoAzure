namespace Lab.Api.Domain.Entities
{
    public class RangoEdad
    {
        public long IdRangoEdad { get; set; }
        public string Descripcion { get; set; } = null!;
        public int EdadMinDias { get; set; }
        public int? EdadMaxDias { get; set; }
    }
}
