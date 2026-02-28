namespace Lab.Api.Domain.Entities
{
    public class FacturaDetalle
    {
        public long IdFacturaDetalle { get; set; }
        public long IdFactura { get; set; }
        public long IdExamenSolicitado { get; set; }
        public long IdCUPS { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
