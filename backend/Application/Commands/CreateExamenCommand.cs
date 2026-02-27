namespace Lab.Api.Application.Commands
{
    public class CreateExamenCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public long IdArea { get; set; }
        public string CodigoExamen { get; set; } = null!;
        public string NombreExamen { get; set; } = null!;
        public long? IdCUPS { get; set; }
        public decimal PrecioBase { get; set; }
        public int? TiempoEntregaHoras { get; set; }
    }
}
