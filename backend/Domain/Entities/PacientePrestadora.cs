namespace Lab.Api.Domain.Entities
{
    public class PacientePrestadora
    {
        public long IdPacientePrestadora { get; set; }
        public long IdPaciente { get; set; }
        public long IdTipoAfiliacion { get; set; }
        public string? NroCuenta { get; set; }
    }
}
