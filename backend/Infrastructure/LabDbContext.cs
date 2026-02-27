using Microsoft.EntityFrameworkCore;
using Lab.Api.Domain.Entities;

namespace Lab.Api.Infrastructure
{
    public class LabDbContext : DbContext
    {
        public LabDbContext(DbContextOptions<LabDbContext> options) : base(options) { }

        public DbSet<Persona> Persona { get; set; } = null!;
        public DbSet<Paciente> Paciente { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(b =>
            {
                b.ToTable("Persona");
                b.HasKey(p => p.IdPersona);
                b.Property(p => p.IdPersona).ValueGeneratedOnAdd();
                b.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
                b.Property(p => p.PrimerApellido).HasMaxLength(50).IsRequired();
                b.Property(p => p.NroIdentificacion).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<Paciente>(b =>
            {
                b.ToTable("Paciente");
                b.HasKey(p => p.IdPaciente);
                b.Property(p => p.IdPaciente).ValueGeneratedOnAdd();
                b.HasOne(p => p.Persona).WithMany().HasForeignKey(p => p.IdPersona);
            });
        }
    }
}
