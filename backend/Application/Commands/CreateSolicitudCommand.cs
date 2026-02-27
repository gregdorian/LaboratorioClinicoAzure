using System;
using System.Collections.Generic;

namespace Lab.Api.Application.Commands
{
    public class CreateSolicitudCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public long IdPaciente { get; set; }
        public long? IdMedico { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public List<CreateExamenSolicitadoItem> Examenes { get; set; } = new();
    }

    public class CreateExamenSolicitadoItem
    {
        public long IdExamen { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal ValorUnitario { get; set; }
    }
}
