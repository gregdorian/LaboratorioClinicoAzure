namespace Lab.Api.Application.DTOs
{
    public class CUPSDto
    {
        public long IdCUPS { get; set; }
        public string CodigoCUPS { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoServicio { get; set; } = null!;
    }
}
