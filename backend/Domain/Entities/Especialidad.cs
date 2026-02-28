namespace Lab.Api.Domain.Entities
{
    public class Especialidad
    {
        public long IdEspecialidad { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
