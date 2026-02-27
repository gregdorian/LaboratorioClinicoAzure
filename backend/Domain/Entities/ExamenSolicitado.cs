namespace Lab.Api.Domain.Entities
{
    public class ExamenSolicitado
    {
        public long IdExamenSolicitado { get; set; }
        public long IdSolicitud { get; set; }
        public long IdExamen { get; set; }
        public long? IdCUPS { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
