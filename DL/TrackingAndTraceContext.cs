using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class TrackingAndTraceContext : DbContext
{
    public TrackingAndTraceContext()
    {
    }

    public TrackingAndTraceContext(DbContextOptions<TrackingAndTraceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<EstatusEntrega> EstatusEntregas { get; set; }

    public virtual DbSet<EstatusUnidad> EstatusUnidads { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<Repartidor> Repartidors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<UnidadAsignadum> UnidadAsignada { get; set; }

    public virtual DbSet<UnidadEntrega> UnidadEntregas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-15TRT047; Database= TrackingAndTrace; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__C852F553F4514D1D");

            entity.ToTable("Entrega");

            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdEstatusEntregaNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEstatusEntrega)
                .HasConstraintName("FK__Entrega__IdEstat__2A4B4B5E");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__Entrega__IdPaque__286302EC");

            entity.HasOne(d => d.IdRepartidorNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdRepartidor)
                .HasConstraintName("FK__Entrega__IdRepar__29572725");
        });

        modelBuilder.Entity<EstatusEntrega>(entity =>
        {
            entity.HasKey(e => e.IdEstatusEntrega).HasName("PK__EstatusE__0C56D2D07472D0CD");

            entity.ToTable("EstatusEntrega");

            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.EstatusEntregas)
                .HasForeignKey(d => d.IdEntrega)
                .HasConstraintName("FK__EstatusEn__IdEnt__3D5E1FD2");
        });

        modelBuilder.Entity<EstatusUnidad>(entity =>
        {
            entity.HasKey(e => e.IdEstatusUnidad).HasName("PK__EstatusU__0CBBD6296AD7FC39");

            entity.ToTable("EstatusUnidad");
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquete__DE278F8B0F98770D");

            entity.ToTable("Paquete", tb => tb.HasTrigger("CrearPaquete"));

            entity.Property(e => e.CodigoRastreo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Detalle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DireccionEntrega).IsUnicode(false);
            entity.Property(e => e.DireccionOrigen).IsUnicode(false);
            entity.Property(e => e.FechaEstimadaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.Paquetes)
                .HasForeignKey(d => d.IdEntrega)
                .HasConstraintName("FK__Paquete__IdEntre__3B75D760");
        });

        modelBuilder.Entity<Repartidor>(entity =>
        {
            entity.HasKey(e => e.IdRepartidor).HasName("PK__Repartid__BF0B3B9AFD93DA80");

            entity.ToTable("Repartidor");

            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.Fotografia).IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdEntrega)
                .HasConstraintName("FK__Repartido__IdEnt__3C69FB99");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("FK__Repartido__IdUni__3E52440B");

            entity.HasOne(d => d.IdUnidadAsignadaNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdUnidadAsignada)
                .HasConstraintName("FK__Repartido__IdUni__22AA2996");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Repartidors)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Repartido__IdUsu__3F466844");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CFEB46301");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadAsignadum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadAsignada).HasName("PK__UnidadAs__9EFA213DE17F6DF2");

            entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            entity.Property(e => e.Solicitud)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadEntrega>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK__UnidadEn__437725E67EAB46B5");

            entity.ToTable("UnidadEntrega");

            entity.Property(e => e.AñoFabricacion)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Marca)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusUnidadNavigation).WithMany(p => p.UnidadEntregas)
                .HasForeignKey(d => d.IdEstatusUnidad)
                .HasConstraintName("FK__UnidadEnt__IdEst__25869641");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF978D11287E");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
