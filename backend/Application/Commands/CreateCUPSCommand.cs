namespace Lab.Api.Application.Commands
{
    public class CreateCUPSCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public string CodigoCUPS { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoServicio { get; set; } = null!;
        public string? UnidadMedida { get; set; }
        public byte? NivelAtencion { get; set; }
    }
}
