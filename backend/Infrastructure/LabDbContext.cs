using Microsoft.EntityFrameworkCore;
using Lab.Api.Domain.Entities;

namespace Lab.Api.Infrastructure
{
    public class LabDbContext : DbContext
    {
        public LabDbContext(DbContextOptions<LabDbContext> options) : base(options) { }

        public DbSet<Persona> Persona { get; set; } = null!;
        public DbSet<Paciente> Paciente { get; set; } = null!;
        public DbSet<CUPS> CUPS { get; set; } = null!;
        public DbSet<Examen> Examenes { get; set; } = null!;
        public DbSet<SolicitudExamen> SolicitudesExamen { get; set; } = null!;
        public DbSet<ExamenSolicitado> ExamenesSolicitados { get; set; } = null!;
        public DbSet<Factura> Factura { get; set; } = null!;

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

            modelBuilder.Entity<CUPS>(b =>
            {
                b.ToTable("CUPS");
                b.HasKey(c => c.IdCUPS);
                b.Property(c => c.IdCUPS).ValueGeneratedOnAdd();
                b.Property(c => c.CodigoCUPS).HasMaxLength(20).IsRequired();
                b.Property(c => c.Descripcion).HasMaxLength(300).IsRequired();
            });

            modelBuilder.Entity<Examen>(b =>
            {
                b.ToTable("Examenes");
                b.HasKey(e => e.IdExamen);
                b.Property(e => e.IdExamen).ValueGeneratedOnAdd();
                b.Property(e => e.CodigoExamen).HasMaxLength(20).IsRequired();
                b.Property(e => e.NombreExamen).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<SolicitudExamen>(b =>
            {
                b.ToTable("SolicitudesExamen");
                b.HasKey(s => s.IdSolicitud);
                b.Property(s => s.IdSolicitud).ValueGeneratedOnAdd();
                b.Property(s => s.NroOrden).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<ExamenSolicitado>(b =>
            {
                b.ToTable("ExamenesSolicitados");
                b.HasKey(es => es.IdExamenSolicitado);
                b.Property(es => es.IdExamenSolicitado).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Factura>(b =>
            {
                b.ToTable("Factura");
                b.HasKey(f => f.IdFactura);
                b.Property(f => f.IdFactura).ValueGeneratedOnAdd();
                b.Property(f => f.NroFactura).HasMaxLength(30).IsRequired();
            });
        }
    }
}
