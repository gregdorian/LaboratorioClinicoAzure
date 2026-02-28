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
        public DbSet<DisponibilidadHoraria> DisponibilidadHoraria { get; set; } = null!;
        public DbSet<Cita> Citas { get; set; } = null!;
        public DbSet<NotificacionCita> NotificacionesCita { get; set; } = null!;
        public DbSet<RipsTransaccional> RipsTransaccional { get; set; } = null!;
        public DbSet<RipsLoteControl> RipsLoteControl { get; set; } = null!;
        public DbSet<Direccion> Direccion { get; set; } = null!;
        public DbSet<EntidadPagadora> EntidadPagadora { get; set; } = null!;
        public DbSet<Prueba> Pruebas { get; set; } = null!;
        public DbSet<ValoresReferencia> ValoresReferencia { get; set; } = null!;
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; } = null!;
        public DbSet<PrestadoraSalud> PrestadorasSalud { get; set; } = null!;
        public DbSet<TipoAfiliacion> TiposAfiliacion { get; set; } = null!;
        public DbSet<Sexo> Sexos { get; set; } = null!;
        public DbSet<TipoDocumento> TiposDocumento { get; set; } = null!;
        public DbSet<TipoRegimen> TiposRegimen { get; set; } = null!;
        public DbSet<TipoIngreso> TiposIngreso { get; set; } = null!;
        public DbSet<Diagnostico> Diagnosticos { get; set; } = null!;
        public DbSet<Especialidad> Especialidades { get; set; } = null!;
        public DbSet<TipoMuestra> TiposMuestra { get; set; } = null!;
        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<ExamenMuestraConfig> ExamenMuestraConfig { get; set; } = null!;
        public DbSet<ExamenMuestra> ExamenMuestra { get; set; } = null!;
        public DbSet<Resultado> Resultados { get; set; } = null!;
        public DbSet<Contrato> Contrato { get; set; } = null!;
        public DbSet<Tarifario> Tarifario { get; set; } = null!;
        public DbSet<PacientePrestadora> PacientePrestadora { get; set; } = null!;
        public DbSet<Autorizacion> Autorizacion { get; set; } = null!;
        public DbSet<FacturaElectronica> FacturaElectronica { get; set; } = null!;
        public DbSet<Glosa> Glosa { get; set; } = null!;
        public DbSet<AuditLog> AuditLog { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<DisponibilidadHoraria>(b =>
            {
                b.ToTable("DisponibilidadHoraria");
                b.HasKey(d => d.IdDisponibilidad);
                b.Property(d => d.IdDisponibilidad).ValueGeneratedOnAdd();
                b.Property(d => d.CodigoSede).HasMaxLength(20).IsRequired();
                b.Property(d => d.RowVer).IsRowVersion();
            });

            modelBuilder.Entity<Cita>(b =>
            {
                b.ToTable("Citas");
                b.HasKey(c => c.IdCita);
                b.Property(c => c.IdCita).ValueGeneratedOnAdd();
                b.Property(c => c.Motivo).HasMaxLength(500);
            });

            modelBuilder.Entity<NotificacionCita>(b =>
            {
                b.ToTable("NotificacionesCita");
                b.HasKey(n => n.IdNotificacion);
                b.Property(n => n.IdNotificacion).ValueGeneratedOnAdd();
                b.Property(n => n.TipoNotif).HasMaxLength(50).IsRequired();
                b.Property(n => n.Destinatario).HasMaxLength(100).IsRequired();
                b.Property(n => n.Asunto).HasMaxLength(200).IsRequired();
                b.Property(n => n.MensajeError).HasMaxLength(500);
            });

            modelBuilder.Entity<RipsTransaccional>(b =>
            {
                b.ToTable("RIPS_Transaccional");
                b.HasKey(r => r.IdRips);
                b.Property(r => r.IdRips).ValueGeneratedOnAdd();
                b.Property(r => r.ConsecutivoRips).HasMaxLength(20).IsRequired();
                b.Property(r => r.CodigoCups).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<Direccion>(b =>
            {
                b.ToTable("Direccion");
                b.HasKey(d => d.IdDireccion);
                b.Property(d => d.IdDireccion).ValueGeneratedOnAdd();
                b.Property(d => d.Pais).HasMaxLength(60).IsRequired();
                b.Property(d => d.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<EntidadPagadora>(b =>
            {
                b.ToTable("EntidadPagadora");
                b.HasKey(e => e.IdEntidadPagadora);
                b.Property(e => e.IdEntidadPagadora).ValueGeneratedOnAdd();
                b.Property(e => e.CodigoEntidad).HasMaxLength(20).IsRequired();
                b.Property(e => e.Nombre).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<Prueba>(b =>
            {
                b.ToTable("Pruebas");
                b.HasKey(p => p.IdPrueba);
                b.Property(p => p.IdPrueba).ValueGeneratedOnAdd();
                b.Property(p => p.NombrePrueba).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<ValoresReferencia>(b =>
            {
                b.ToTable("ValoresReferencia");
                b.HasKey(v => v.IdValorReferencia);
                b.Property(v => v.IdValorReferencia).ValueGeneratedOnAdd();
                b.Property(v => v.Unidad).HasMaxLength(30);
            });

            modelBuilder.Entity<FacturaDetalle>(b =>
            {
                b.ToTable("FacturaDetalle");
                b.HasKey(f => f.IdFacturaDetalle);
                b.Property(f => f.IdFacturaDetalle).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PrestadoraSalud>(b =>
            {
                b.ToTable("PrestadorasSalud");
                b.HasKey(p => p.IdPrestadora);
                b.Property(p => p.IdPrestadora).ValueGeneratedOnAdd();
                b.Property(p => p.Codigo).HasMaxLength(10).IsRequired();
                b.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<TipoAfiliacion>(b =>
            {
                b.ToTable("TiposAfiliacion");
                b.HasKey(t => t.IdTipoAfiliacion);
                b.Property(t => t.IdTipoAfiliacion).ValueGeneratedOnAdd();
                b.Property(t => t.Codigo).HasMaxLength(10).IsRequired();
                b.Property(t => t.Descripcion).HasMaxLength(60).IsRequired();
            });

            modelBuilder.Entity<Sexo>(b =>
            {
                b.ToTable("Sexos");
                b.HasKey(s => s.IdSexo);
                b.Property(s => s.IdSexo).HasMaxLength(1).IsRequired();
                b.Property(s => s.Descripcion).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<TipoDocumento>(b =>
            {
                b.ToTable("TiposDocumento");
                b.HasKey(t => t.IdTipoDocumento);
                b.Property(t => t.IdTipoDocumento).HasMaxLength(5).IsRequired();
            });

            modelBuilder.Entity<TipoRegimen>(b =>
            {
                b.ToTable("TiposRegimen");
                b.HasKey(t => t.IdTipoRegimen);
            });

            modelBuilder.Entity<TipoIngreso>(b =>
            {
                b.ToTable("TiposIngreso");
                b.HasKey(t => t.IdTipoIngreso);
            });

            modelBuilder.Entity<Diagnostico>(b =>
            {
                b.ToTable("Diagnosticos");
                b.HasKey(d => d.IdDiagnostico);
            });

            modelBuilder.Entity<Especialidad>(b =>
            {
                b.ToTable("Especialidades");
                b.HasKey(e => e.IdEspecialidad);
            });

            modelBuilder.Entity<TipoMuestra>(b =>
            {
                b.ToTable("TiposMuestra");
                b.HasKey(t => t.IdTipoMuestra);
            });

            modelBuilder.Entity<Area>(b =>
            {
                b.ToTable("Areas");
                b.HasKey(a => a.IdArea);
            });

            modelBuilder.Entity<ExamenMuestraConfig>(b =>
            {
                b.ToTable("ExamenMuestraConfig");
                b.HasKey(x => x.IdExamenMuestraConfig);
                b.Property(x => x.IdExamenMuestraConfig).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ExamenMuestra>(b =>
            {
                b.ToTable("ExamenMuestra");
                b.HasKey(x => x.IdExamenMuestra);
                b.Property(x => x.IdExamenMuestra).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Resultado>(b =>
            {
                b.ToTable("Resultados");
                b.HasKey(r => r.IdResultado);
                b.Property(r => r.IdResultado).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Contrato>(b =>
            {
                b.ToTable("Contrato");
                b.HasKey(c => c.IdContrato);
                b.Property(c => c.IdContrato).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Tarifario>(b =>
            {
                b.ToTable("Tarifario");
                b.HasKey(t => t.IdTarifario);
                b.Property(t => t.IdTarifario).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PacientePrestadora>(b =>
            {
                b.ToTable("PacientePrestadora");
                b.HasKey(p => p.IdPacientePrestadora);
                b.Property(p => p.IdPacientePrestadora).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Autorizacion>(b =>
            {
                b.ToTable("Autorizacion");
                b.HasKey(a => a.IdAutorizacion);
                b.Property(a => a.IdAutorizacion).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FacturaElectronica>(b =>
            {
                b.ToTable("FacturaElectronica");
                b.HasKey(f => f.IdFacturaElectronica);
                b.Property(f => f.IdFacturaElectronica).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Glosa>(b =>
            {
                b.ToTable("Glosa");
                b.HasKey(g => g.IdGlosa);
                b.Property(g => g.IdGlosa).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.ToTable("AuditLog");
                b.HasKey(a => a.IdAudit);
                b.Property(a => a.IdAudit).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).ValueGeneratedOnAdd();
                b.Property(u => u.Username).HasMaxLength(100).IsRequired();
                b.Property(u => u.Email).HasMaxLength(150);
                b.Property(u => u.PasswordHash).HasMaxLength(300).IsRequired();
                b.Property(u => u.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<RipsLoteControl>(b =>
            {
                b.ToTable("RIPS_LoteControl");
                b.HasKey(l => l.IdLote);
                b.Property(l => l.IdLote).ValueGeneratedOnAdd();
                b.Property(l => l.NumeroLote).HasMaxLength(30).IsRequired();
            });
        }
    }
}
