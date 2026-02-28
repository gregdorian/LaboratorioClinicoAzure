using System;

namespace Lab.Api.Application.Commands
{
    public class GenerarLoteRipsCommand : Lab.Api.Application.CQRS.ICommand<long>
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; } = null!;
    }
}
