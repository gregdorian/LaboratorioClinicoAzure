using System;

namespace Lab.Api.Domain.Entities
{
    public class Resultado
    {
        public long IdResultado { get; set; }
        public long IdExamenSolicitado { get; set; }
        public long IdExamenMuestra { get; set; }
        public long IdPrueba { get; set; }
        public long? IdValorReferencia { get; set; }
        public long IdPersonalLaboratorio { get; set; }
        public string? ResultadoValor { get; set; }
        public string? ResultadoTexto { get; set; }
        public string? Unidad { get; set; }
        public bool EstaFueraRango { get; set; }
        public bool EsValidado { get; set; }
        public DateTime FechaResultado { get; set; }
    }
}
