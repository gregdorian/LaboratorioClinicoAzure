using System;

namespace Lab.Api.Application.Commands
{
    public class PublishSlotsCommand : Lab.Api.Application.CQRS.ICommand<int>
    {
        public string CodigoSede { get; set; } = "PRINCIPAL";
        public long? IdPersonalLaboratorio { get; set; }
        public DateTime FechaInicio { get; set; }
        public int CantidadSlots { get; set; }
        public int DuracionMinutos { get; set; } = 30;
        public int CupoMaximoPorSlot { get; set; } = 1;
    }
}
