using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.DAL.DBContext;

public partial class BasePlanificacionContext : DbContext
{
    public BasePlanificacionContext()
    {
    }

    public BasePlanificacionContext(DbContextOptions<BasePlanificacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividads { get; set; }

    public virtual DbSet<CentroSalud> CentroSaluds { get; set; }

    public virtual DbSet<CierreCarpeta> CierreCarpeta { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<DetallePlanificacion> DetallePlanificacions { get; set; }

    public virtual DbSet<DocmCompra> DocmCompras { get; set; }

    public virtual DbSet<DocmPlanificacion> DocmPlanificacions { get; set; }

    public virtual DbSet<DocmPresupuesto> DocmPresupuestos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MoviPlanificacion> MoviPlanificacions { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; }

    public virtual DbSet<PartidaPresupuestaria> PartidaPresupuestaria { get; set; }

    public virtual DbSet<Planificacion> Planificacions { get; set; }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolGeneral> RolGenerals { get; set; }

    public virtual DbSet<RolMenu> RolMenus { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Transferencia> Transferencias { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.IdActividad);

            entity.ToTable("actividad");

            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CentroSalud>(entity =>
        {
            entity.HasKey(e => e.IdCentro);

            entity.ToTable("centrosalud");

            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CierreCarpeta>(entity =>
        {
            entity.HasKey(e => e.IdPlanificacion);

            entity.ToTable("cierreCarpeta");

            entity.Property(e => e.IdPlanificacion)
                .ValueGeneratedNever()
                .HasColumnName("idPlanificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.PartidaPresupuesto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("partidaPresupuesto");
            entity.Property(e => e.SaldoFinal).HasColumnName("saldoFinal");
            entity.Property(e => e.TipoCierre)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("tipoCierre");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.ToTable("compra");

            entity.Property(e => e.IdCompra)
                .ValueGeneratedNever()
                .HasColumnName("idCompra");
            entity.Property(e => e.CuceCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cuceCompra");
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.FechabienContratado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechabienContratado");
            entity.Property(e => e.IdPlanificacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPlanificacion");
            entity.Property(e => e.ModalidadCompra)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("modalidadCompra");
            entity.Property(e => e.MontoadjudicadoCompra).HasColumnName("montoadjudicadoCompra");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.NrofacturaCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nrofacturaCompra");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ObjContratocompra)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("objContratocompra");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlanifCompra");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("configuracion");

            entity.Property(e => e.Propiedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("propiedad");
            entity.Property(e => e.Recurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("recurso");
            entity.Property(e => e.Valor)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<DetallePlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdDetallePlanificacion);

            entity.ToTable("detallePlanificacion");

            entity.Property(e => e.IdDetallePlanificacion)
                .ValueGeneratedNever()
                .HasColumnName("idDetallePlanificacion");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.MontoCompra).HasColumnName("montoCompra");
            entity.Property(e => e.MontoPlanificacion).HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontoPoa).HasColumnName("montoPoa");
            entity.Property(e => e.MontoPresupuesto).HasColumnName("montoPresupuesto");
            entity.Property(e => e.NombreitemPlanificacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreitemPlanificacion");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.DetallePlanificacions)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK_PartidaPDetaP");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DetallePlanificacions)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_PlanifDetaP");
        });

        modelBuilder.Entity<DocmCompra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.ToTable("docmCompra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.CuceCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cuceCompra");
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.FechacontratoCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacontratoCompra");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.ModalidadCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("modalidadCompra");
            entity.Property(e => e.MontoadjudicadoCompra).HasColumnName("montoadjudicadoCompra");
            entity.Property(e => e.NrofacturaCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nrofacturaCompra");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ObjcontratoCompra)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("objcontratoCompra");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DocmCompras)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_PlanifDCompra");
        });

        modelBuilder.Entity<DocmPlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdPlanificacion);

            entity.ToTable("docmPlanificacion");

            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.CertificadopoaPlanificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certificadopoaPlanificacion");
            entity.Property(e => e.FechaPlanificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPlanificacion");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.MontoPlanificacion).HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontopoaPlanificacion).HasColumnName("montopoaPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ReferenciaPlanificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenciaPlanificacion");
            entity.Property(e => e.UbicacionPlanificacion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ubicacionPlanificacion");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.DocmPlanificacions)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_ActDocmPlanif");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.DocmPlanificacions)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_CensalDocmPlanif");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.DocmPlanificacions)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_EmpDocmPlanif");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.DocmPlanificacions)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_ProgDocmPlanif");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DocmPlanificacions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_UsuaDocmPlanif");
        });

        modelBuilder.Entity<DocmPresupuesto>(entity =>
        {
            entity.HasKey(e => e.IdPresupuesto);

            entity.ToTable("docmPresupuesto");

            entity.Property(e => e.IdPresupuesto).HasColumnName("idPresupuesto");
            entity.Property(e => e.CertPresupuesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certPresupuesto");
            entity.Property(e => e.FechaPresupuesto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPresupuesto");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.MontoPresupuesto).HasColumnName("montoPresupuesto");
            entity.Property(e => e.Nulo).HasColumnName("nulo");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DocmPresupuestos)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_PlanifDPresupu");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.DocmPresupuestos)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_ActDPresupu");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);

            entity.ToTable("empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu);

            entity.ToTable("menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Controlador)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("controlador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Icono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.IdmenuPadre).HasColumnName("idmenuPadre");
            entity.Property(e => e.PaginaAccion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("paginaAccion");

            entity.HasOne(d => d.IdmenuPadreNavigation).WithMany(p => p.InverseIdmenuPadreNavigation)
                .HasForeignKey(d => d.IdmenuPadre)
                .HasConstraintName("FK_IdMenuPadreNavigation");
        });

        modelBuilder.Entity<MoviPlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdMoviPlanificacion);

            entity.ToTable("moviPlanificacion");

            entity.Property(e => e.IdMoviPlanificacion)
                .ValueGeneratedNever()
                .HasColumnName("idMoviPlanificacion");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdPlanificacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPlanificacion");
            entity.Property(e => e.MontocompraPartida).HasColumnName("montocompraPartida");
            entity.Property(e => e.MontoplanificacionPartida).HasColumnName("montoplanificacionPartida");
            entity.Property(e => e.MontopoaPartida).HasColumnName("montopoaPartida");
            entity.Property(e => e.MontopresupuestoPartida).HasColumnName("montopresupuestoPartida");
            entity.Property(e => e.NombreitemPartida)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreitemPartida");
            entity.Property(e => e.Nulo).HasColumnName("nulo");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.MoviPlanificacions)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK_PartidaPMoviPlanif");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.MoviPlanificacions)
                .HasForeignKey(d => d.IdPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocmPlanifMoviPlanif");
        });

        modelBuilder.Entity<NumeroCorrelativo>(entity =>
        {
            entity.HasKey(e => e.IdCorrelativo);

            entity.ToTable("numeroCorrelativo");

            entity.Property(e => e.IdCorrelativo).HasColumnName("idCorrelativo");
            entity.Property(e => e.CantidadDigitos).HasColumnName("cantidadDigitos");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.Gestion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("gestion");
            entity.Property(e => e.UltimonroCorrelativo).HasColumnName("ultimonroCorrelativo");
        });

        modelBuilder.Entity<PartidaPresupuestaria>(entity =>
        {
            entity.HasKey(e => e.IdPartida);

            entity.ToTable("partidaPresupuestaria");

            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Planificacion>(entity =>
        {
            entity.HasKey(e => e.IdPlanificacion);

            entity.ToTable("planificacion");

            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.CertificadoPoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certificadoPoa");
            entity.Property(e => e.FechaPlanificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPlanificacion");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.MontoPlanificacion).HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontopoaPlanificacion).HasColumnName("montopoaPlanificacion");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ReferenciaPlanificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenciaPlanificacion");
            entity.Property(e => e.UbicacionPlanificacion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ubicacionPlanificacion");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_ActPlanif");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_CensalPlanif");

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK_TipoDocmPlanif");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_EmpPlanif");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_ProgPlanif");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_UsuaPlanif");
        });

        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity.HasKey(e => e.IdPresupuesto);

            entity.ToTable("presupuesto");

            entity.Property(e => e.IdPresupuesto)
                .ValueGeneratedNever()
                .HasColumnName("idPresupuesto");
            entity.Property(e => e.CertPresupuesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certPresupuesto");
            entity.Property(e => e.FechaPresupuesto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPresupuesto");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.IdPlanificacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPlanificacion");
            entity.Property(e => e.IdPrograma)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPrograma");
            entity.Property(e => e.MontoPresupuesto).HasColumnName("montoPresupuesto");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.Presupuestos)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_ActPresupu");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.Presupuestos)
                .HasForeignKey(d => d.IdPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlanifPresupu");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Presupuestos)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_ProgPresupu");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.IdPrograma);

            entity.ToTable("programa");

            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.Codigo)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<RolGeneral>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("rolGeneral");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.IdrolMenu);

            entity.ToTable("rolMenu");

            entity.Property(e => e.IdrolMenu).HasColumnName("idrolMenu");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdDocumento);

            entity.ToTable("tipoDocumento");

            entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Transferencia>(entity =>
        {
            entity.HasKey(e => e.IdTransferencia);

            entity.ToTable("transferencias");

            entity.Property(e => e.IdTransferencia).HasColumnName("idTransferencia");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdDestino)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("idDestino");
            entity.Property(e => e.IdOrigen)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("idOrigen");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referencia");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Codigo)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("codigo");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Carnet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("carnet");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreFoto");
            entity.Property(e => e.Profesion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("profesion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlFoto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdRolNavigation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
