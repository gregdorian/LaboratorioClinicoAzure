namespace Lab.Api.Domain.Entities
{
    public class CUPS
    {
        public long IdCUPS { get; set; }
        public string CodigoCUPS { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoServicio { get; set; } = null!;
        public string? UnidadMedida { get; set; }
        public byte? NivelAtencion { get; set; }
    }
}
