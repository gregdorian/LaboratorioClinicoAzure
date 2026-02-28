using System;

namespace Lab.Api.Application.Commands
{
    public class GenerarRipsCommand : Lab.Api.Application.CQRS.ICommand<int>
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public long? IdEntidad { get; set; }
        public string? Usuario { get; set; }
    }
}
