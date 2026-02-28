namespace Lab.Api.Domain.Entities
{
    public class Prueba
    {
        public long IdPrueba { get; set; }
        public long IdExamen { get; set; }
        public string NombrePrueba { get; set; } = null!;
        public int Orden { get; set; }
    }
}
