namespace Lab.Api.Domain.Entities
{
    public class Examen
    {
        public long IdExamen { get; set; }
        public long IdArea { get; set; }
        public string CodigoExamen { get; set; } = null!;
        public string NombreExamen { get; set; } = null!;
        public long? IdCUPS { get; set; }
        public decimal PrecioBase { get; set; }
        public int? TiempoEntregaHoras { get; set; }
    }
}
