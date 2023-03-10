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

    public virtual DbSet<Actividad> Actividad { get; set; }

    public virtual DbSet<CentroSalud> CentroSalud { get; set; }

    public virtual DbSet<CierreCarpeta> CierreCarpeta { get; set; }

    public virtual DbSet<Compra> Compra { get; set; }

    public virtual DbSet<Configuracion> Configuracion { get; set; }

    public virtual DbSet<DetallePlanificacion> DetallePlanificacion { get; set; }

    public virtual DbSet<DocmCompra> DocmCompra { get; set; }

    public virtual DbSet<DocmPlanificacion> DocmPlanificacion { get; set; }

    public virtual DbSet<DocmPresupuesto> DocmPresupuesto { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<MoviPlanificacion> MoviPlanificacion { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativo { get; set; }

    public virtual DbSet<Negocio> Negocio { get; set; }
    public virtual DbSet<Objetivo> Objetivo { get; set; }
    public virtual DbSet<Operacion> Operacion { get; set; }
    public virtual DbSet<PartidaPresupuestaria> PartidaPresupuestaria { get; set; }

    public virtual DbSet<Planificacion> Planificacion { get; set; }

    public virtual DbSet<Presupuesto> Presupuesto { get; set; }

    public virtual DbSet<Programa> Programa { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<RolGeneral> RolGeneral { get; set; }

    public virtual DbSet<RolMenu> RolMenu { get; set; }
    public virtual DbSet<TablaAce> TablaAce { get; set; }
    public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }

    public virtual DbSet<Transferencia> Transferencia { get; set; }
    public virtual DbSet<UnidadResponsable> UnidadResponsable { get; set; }
    public virtual DbSet<Usuario> Usuario { get; set; }
    public virtual DbSet<UnidadProceso> UnidadProceso { get; set; }

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
            entity.Property(e => e.CodigoUnidad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigoUnidad");

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

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.Compra)
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
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.NombreItem)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreItem");
            entity.Property(e => e.Medida)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("medida");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.NumeroPlanificacion)
                .HasMaxLength(6)
                .HasColumnName("numeroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");

            entity.HasOne(d => d.IdDetallePlanificacionPartidaNavigation).WithMany(p => p.DetallePlanificacion)
                .HasForeignKey(d => d.IdDetallePlanificacion)
                .HasConstraintName("FK_IdDetallePlanificacionPartidaNavigation");

            entity.HasOne(d => d.IdDetallePlanificacionPlanificacionNavigation).WithMany(p => p.DetallePlanificacion)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_IdDetallePlanificacionPlanificacionNavigation");

            //entity.HasOne(d => d.IdDetallePlanificacionActividadNavigation).WithMany(p => p.DetallePlanificacion)
            //    .HasForeignKey(d => d.DetallePlanificacion)
            //    .HasConstraintName("FK_IdDetallePlanificacionActividadNavigation");
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

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DocmCompra)
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

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.DocmPlanificacion)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_ActDocmPlanif");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.DocmPlanificacion)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_CensalDocmPlanif");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.DocmPlanificacion)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_EmpDocmPlanif");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.DocmPlanificacion)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_ProgDocmPlanif");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DocmPlanificacion)
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

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DocmPresupuesto)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_PlanifDPresupu");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.DocmPresupuesto)
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

        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.IdNegocio);

            entity.ToTable("negocio");

            entity.Property(e => e.IdNegocio).HasColumnName("idNegocio");
            entity.Property(e => e.Correo)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreLogo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreLogo");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.PorcentajeImpuesto).HasColumnName("porcentajeImpuesto");
            entity.Property(e => e.SimboloMoneda)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("simboloMoneda");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlLogo");
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
            entity.Property(e => e.Ultimonumero).HasColumnName("ultimonumero");
        });

        modelBuilder.Entity<Objetivo>(entity =>
        {
            entity.HasKey(e => e.IdObjetivo);

            entity.ToTable("objetivo");

            entity.Property(e => e.IdObjetivo).HasColumnName("idObjetivo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
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

        modelBuilder.Entity<Operacion>(entity =>
        {
            entity.HasKey(e => e.IdOperacion);

            entity.ToTable("operacion");

            entity.Property(e => e.IdOperacion).HasColumnName("idOperacion");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
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

        modelBuilder.Entity<PartidaPresupuestaria>(entity =>
        {
            entity.HasKey(e => e.IdPartida);

            entity.ToTable("partidaPresupuestaria");

            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
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
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");


            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.PartidaPresupuestaria)
                .HasForeignKey(d => d.IdPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdProgramaNavigation");
        });

        modelBuilder.Entity<Planificacion>(entity =>
        {
            entity.HasKey(e => e.IdPlanificacion);

            entity.ToTable("planificacion");

            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.NumeroPlanificacion)
                .HasMaxLength(6)
                .HasColumnName("numeroPlanificacion");
            entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");
            entity.Property(e => e.CitePlanificacion)
                .HasMaxLength(30)
                .HasColumnName("citePlanificacion");
            entity.Property(e => e.Lugar)
                .HasMaxLength(20)
                .HasColumnName("lugar");
            entity.Property(e => e.FechaPlanificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPlanificacion");
            entity.Property(e => e.CertificadoPoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certificadoPoa");
            entity.Property(e => e.ReferenciaPlanificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenciaPlanificacion");
            entity.Property(e => e.NombreRegional)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreRegional");
            entity.Property(e => e.NombreEjecutora)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreEjecutora");
            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdResponsable).HasColumnName("idResponsable");
            entity.Property(e => e.IdResponsable).HasColumnName("idUnidadProceso");

            entity.Property(e => e.MontoPlanificacion).HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontoPoa).HasColumnName("montoPoa");
            entity.Property(e => e.MontoPresupuesto).HasColumnName("montoPresupuesto");
            entity.Property(e => e.MontoCompra).HasColumnName("montoCompra");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.UbicacionPlanificacion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ubicacionPlanificacion");

            entity.HasOne(d => d.IdPlanificacionTipoDocumentoNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK_IdPlanificacionTipoDocumentoNavigation");

            entity.HasOne(d => d.IdPlanificacionActividadNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_IdPlanificacionActividadNavigation");

            entity.HasOne(d => d.IdPlanificacionCentroNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_IdPlanificacionCentroNavigation");

            entity.HasOne(d => d.IdPlanificacionEmpresaNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_IdPlanificacionEmpresaNavigation");

            entity.HasOne(d => d.IdPlanificacionProgramaNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_IdPlanificacionProgramaNavigation");

            entity.HasOne(d => d.IdPlanificacionUsuarioNavigation).WithMany(p => p.Planificacion)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_IdPlanificacionUsuarioNavigation");

            //entity.HasOne(d => d.IdPlanificacionUnidadProcesoNavigation).WithMany(p => p.Planificacion)
            //    .HasForeignKey(d => d.IdUnidadProceso)
            //    .HasConstraintName("FK_IdPlanificacionUnidadProcesoNavigation");

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

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.Presupuesto)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK_ActPresupu");

            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.Presupuesto)
                .HasForeignKey(d => d.IdPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlanifPresupu");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Presupuesto)
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

        modelBuilder.Entity<TablaAce>(entity =>
        {
            entity.HasKey(e => e.IdAce);

            entity.ToTable("tablaAce");

            entity.Property(e => e.IdAce).HasColumnName("idAce");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
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

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdRolNavigation");
        });

        modelBuilder.Entity<UnidadResponsable>(entity =>
        {
            entity.HasKey(e => e.IdUnidad);

            entity.ToTable("unidadResponsable");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
