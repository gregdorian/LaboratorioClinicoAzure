namespace Lab.Api.Domain.Entities
{
    public class Direccion
    {
        public long IdDireccion { get; set; }
        public string? TipoVia { get; set; }
        public string? NombreVia { get; set; }
        public string? NumeroPuerta { get; set; }
        public string? Ciudad { get; set; }
        public string Pais { get; set; } = "Colombia";
        public string? Email { get; set; }
    }
}
