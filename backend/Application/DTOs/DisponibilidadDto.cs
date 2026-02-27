using System;

namespace Lab.Api.Application.DTOs
{
    public class DisponibilidadDto
    {
        public long IdDisponibilidad { get; set; }
        public string CodigoSede { get; set; } = null!;
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; }
        public int CupoMaximo { get; set; }
        public int CuposOcupados { get; set; }
        public byte[] RowVer { get; set; } = null!;
    }
}
