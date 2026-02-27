using System;

namespace Lab.Api.Domain.Entities
{
    public class DisponibilidadHoraria
    {
        public long IdDisponibilidad { get; set; }
        public string CodigoSede { get; set; } = "PRINCIPAL";
        public long? IdPersonalLaboratorio { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; } = 30;
        public int CupoMaximo { get; set; } = 1;
        public int CuposOcupados { get; set; } = 0;
        public bool Activo { get; set; } = true;
        public byte[] RowVer { get; set; } = null!; // rowversion
    }
}
