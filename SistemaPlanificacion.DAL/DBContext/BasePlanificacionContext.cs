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
    public virtual DbSet<AnteproyectoPoa> AnteproyectoPoas { get; set; }

    public virtual DbSet<CentroSalud> CentroSaluds { get; set; }

    public virtual DbSet<CertificacionPlanificacion> CertificacionPlanificacions { get; set; }

    public virtual DbSet<CierreCarpeta> CierreCarpeta { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }
    public virtual DbSet<DetalleAnteproyectoPoa> DetalleAnteproyectoPoas { get; set; }

    public virtual DbSet<DetalleCertificacionPlanificacion> DetalleCertificacionPlanificacions { get; set; }

    public virtual DbSet<DetallePlanificacion> DetallePlanificacions { get; set; }

    public virtual DbSet<DetalleRequerimientoPoa> DetalleRequerimientoPoas { get; set; }

    public virtual DbSet<DocmCompra> DocmCompras { get; set; }

    public virtual DbSet<DocmPlanificacion> DocmPlanificacions { get; set; }

    public virtual DbSet<DocmPresupuesto> DocmPresupuestos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MoviPlanificacion> MoviPlanificacions { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; }
    public virtual DbSet<NumeroCorrelativoPoa> NumeroCorrelativoPoas { get; set; }

    public virtual DbSet<Objetivo> Objetivos { get; set; }

    public virtual DbSet<Operacion> Operacions { get; set; }

    public virtual DbSet<PartidaPresupuestaria> PartidaPresupuestaria { get; set; }

    public virtual DbSet<Planificacion> Planificacions { get; set; }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<RequerimientoPoa> RequerimientoPoas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolGeneral> RolGenerals { get; set; }

    public virtual DbSet<RolMenu> RolMenus { get; set; }

    public virtual DbSet<TablaAce> TablaAces { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Transferencia> Transferencias { get; set; }

    public virtual DbSet<UnidadProceso> UnidadProcesos { get; set; }

    public virtual DbSet<UnidadResponsable> UnidadResponsables { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AnteproyectoPoa>(entity =>
        {
            entity.HasKey(e => e.IdAnteproyecto);

            entity.ToTable("anteproyectoPoa");

            entity.Property(e => e.IdAnteproyecto).HasColumnName("idAnteproyecto");
            entity.Property(e => e.CiteAnteproyecto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("citeAnteproyecto");
            entity.Property(e => e.EstadoAnteproyecto)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("estadoAnteproyecto");
            entity.Property(e => e.FechaAnulacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAnulacion");
            entity.Property(e => e.FechaAnteproyecto)
                .HasColumnType("datetime")
                .HasColumnName("fechaAnteproyecto");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdUnidadResponsable).HasColumnName("idUnidadResponsable");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Lugar)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("lugar");
            entity.Property(e => e.MontoAnteproyecto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoAnteproyecto");
            entity.Property(e => e.NombreEjecutora)
                .HasMaxLength(75)
                .IsFixedLength()
                .HasColumnName("nombreEjecutora");
            entity.Property(e => e.NombreRegional)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombreRegional");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.AnteproyectoPoas)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_AnteproyectoPoa_centroSalud");

            entity.HasOne(d => d.IdUnidadResponsableNavigation).WithMany(p => p.AnteproyectoPoas)
                .HasForeignKey(d => d.IdUnidadResponsable)
                .HasConstraintName("FK_AnteproyectoPoa_unidadResponsable");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AnteproyectoPoas)
                 .HasForeignKey(d => d.IdUsuario)
                 .HasConstraintName("FK_AnteproyectoPoa_Usuario");
        });

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
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CentroSalud>(entity =>
        {
            entity.HasKey(e => e.IdCentro);

            entity.ToTable("centroSalud");

            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
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
        });
        modelBuilder.Entity<CertificacionPlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdCertificacionPlanificacion).HasName("PK_CertificacionPlanificacion_1");

            entity.ToTable("CertificacionPlanificacion");

            entity.Property(e => e.IdCertificacionPlanificacion).HasColumnName("idCertificacionPlanificacion");
            entity.Property(e => e.CodigoPlanificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoPlanificacion");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.TotalCertificado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalCertificado");
            entity.Property(e => e.EstadoCertificacion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("estadoCertificacion");

            /*entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.CertificacionPlanificacions)
                .HasForeignKey(d => d.IdPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertificacionPlanificacion_Planificacion");*/
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
            entity.Property(e => e.MontoadjudicadoCompra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoadjudicadoCompra");
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

        modelBuilder.Entity<DetalleAnteproyectoPoa>(entity =>
        {
            entity.HasKey(e => e.IdDetalleAnteproyecto);

            entity.ToTable("detalleAnteproyectoPoa");

            entity.Property(e => e.IdDetalleAnteproyecto).HasColumnName("idDetalleAnteproyecto");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CodigoActividad).HasColumnName("codigoActividad");
            entity.Property(e => e.Detalle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdAnteproyecto).HasColumnName("idAnteproyecto");
            entity.Property(e => e.Medida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medida");
            entity.Property(e => e.MesAbr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesAbr");
            entity.Property(e => e.MesAgo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesAgo");
            entity.Property(e => e.MesDic)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesDic");
            entity.Property(e => e.MesEne)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesEne");
            entity.Property(e => e.MesFeb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesFeb");
            entity.Property(e => e.MesJul)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesJul");
            entity.Property(e => e.MesJun)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesJun");
            entity.Property(e => e.MesMar)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesMar");
            entity.Property(e => e.MesMay)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesMay");
            entity.Property(e => e.MesNov)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesNov");
            entity.Property(e => e.MesOct)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesOct");
            entity.Property(e => e.MesSep)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesSep");
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.DetalleAnteproyectoPoas)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK_DA_partidaNavigation");
            entity.HasOne(d => d.IdAnteproyectoPoaNavigation).WithMany(p => p.DetalleAnteproyectoPoas)
                .HasForeignKey(d => d.IdAnteproyecto)
                .HasConstraintName("FK_DA_AnteproyectopoaNavigation");
        });

        modelBuilder.Entity<DetalleCertificacionPlanificacion>(entity =>
        {
            entity.HasKey(e => new { e.IdCertificacionPlanificacion, e.IdDetallePlanificacion });

            entity.ToTable("DetalleCertificacionPlanificacion");

            entity.Property(e => e.IdCertificacionPlanificacion).HasColumnName("idCertificacionPlanificacion");
            entity.Property(e => e.IdDetallePlanificacion).HasColumnName("idDetallePlanificacion");
            entity.Property(e => e.MontoPlanificacion)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoPlanificacion");

            entity.HasOne(d => d.IdCertificacionPlanificacionNavigation).WithMany(p => p.DetalleCertificacionPlanificacions)
                .HasForeignKey(d => d.IdCertificacionPlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCertificacionPlanificacion_CertificacionPlanificacion");

           /* entity.HasOne(d => d.IdDetallePlanificacionNavigation).WithMany(p => p.DetalleCertificacionPlanificacions)
                .HasForeignKey(d => d.IdDetallePlanificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCertificacionPlanificacion_DetallePlanificacion");
           */
        });

        modelBuilder.Entity<DetallePlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdDetallePlanificacion).HasName("PK__DetalleP__E75F48A0508352EF");

            entity.ToTable("detallePlanificacion");

            entity.Property(e => e.IdDetallePlanificacion).HasColumnName("idDetallePlanificacion");           
            entity.Property(e => e.CodigoActividad).HasColumnName("codigoActividad");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.Medida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medida");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.NombreItem)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombreItem");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");
            entity.Property(e => e.Temporalidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("temporalidad");
            entity.Property(e => e.Observacion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("observacion");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.DetallePlanificacions)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK_Dp_partidaNavigation");
            entity.HasOne(d => d.IdPlanificacionNavigation).WithMany(p => p.DetallePlanificacions)
                .HasForeignKey(d => d.IdPlanificacion)
                .HasConstraintName("FK_DP_planificacionNavigation");
        });

        modelBuilder.Entity<DetalleRequerimientoPoa>(entity =>
        {
            entity.HasKey(e => e.IdDetalleRequerimientoPoa);

            entity.ToTable("detalleRequerimientoPoa");

            entity.Property(e => e.IdDetalleRequerimientoPoa).HasColumnName("idDetalleRequerimientoPoa");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CodigoActividad).HasColumnName("codigoActividad");
            entity.Property(e => e.Detalle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdRequerimientoPoa).HasColumnName("idRequerimientoPoa");
            entity.Property(e => e.Medida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medida");
            entity.Property(e => e.MesAbr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesAbr");
            entity.Property(e => e.MesAgo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesAgo");
            entity.Property(e => e.MesDic)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesDic");
            entity.Property(e => e.MesEne)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesEne");
            entity.Property(e => e.MesFeb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesFeb");
            entity.Property(e => e.MesJul)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesJul");
            entity.Property(e => e.MesJun)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesJun");
            entity.Property(e => e.MesMar)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesMar");
            entity.Property(e => e.MesMay)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesMay");
            entity.Property(e => e.MesNov)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesNov");
            entity.Property(e => e.MesOct)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesOct");
            entity.Property(e => e.MesSep)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("mesSep");
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.DetalleRequerimientoPoas)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK_DR_partidaNavigation");
            entity.HasOne(d => d.IdRequerimientoPoaNavigation).WithMany(p => p.DetalleRequerimientoPoas)
                .HasForeignKey(d => d.IdRequerimientoPoa)
                .HasConstraintName("FK_DR_requerimientopoaNavigation");
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
            entity.Property(e => e.MontoadjudicadoCompra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoadjudicadoCompra");
            entity.Property(e => e.NrofacturaCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nrofacturaCompra");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ObjcontratoCompra)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("objcontratoCompra");
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
            entity.Property(e => e.MontoPlanificacion)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontopoaPlanificacion)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montopoaPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
            entity.Property(e => e.ReferenciaPlanificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenciaPlanificacion");
            entity.Property(e => e.UbicacionPlanificacion)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ubicacionPlanificacion");
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
            entity.Property(e => e.MontoPresupuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoPresupuesto");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);

            entity.ToTable("empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsFixedLength()
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
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4834FD23706");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Controlador)
                .HasMaxLength(40)
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
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.PaginaAccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paginaAccion");

            entity.HasOne(d => d.IdMenuPadreNavigation).WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .HasConstraintName("FK__Menu__idMenuPadr__36870511");
        });

        modelBuilder.Entity<MoviPlanificacion>(entity =>
        {
            entity.HasKey(e => e.IdMoviPlanificacion);

            entity.ToTable("moviPlanificacion");

            entity.Property(e => e.IdMoviPlanificacion).HasColumnName("idMoviPlanificacion");
            entity.Property(e => e.IdPartida).HasColumnName("idPartida");
            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.MontocompraPartida)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montocompraPartida");
            entity.Property(e => e.MontoplanificacionPartida)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoplanificacionPartida");
            entity.Property(e => e.MontopoaPartida)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montopoaPartida");
            entity.Property(e => e.MontopresupuestoPartida)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montopresupuestoPartida");
            entity.Property(e => e.NombreitemPartida)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombreitemPartida");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.IdNegocio);

            entity.ToTable("Negocio");

            entity.Property(e => e.IdNegocio)
                .ValueGeneratedNever()
                .HasColumnName("idNegocio");
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
            entity.Property(e => e.PorcentajeImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("porcentajeImpuesto");
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
        //---borrar 

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

        modelBuilder.Entity<NumeroCorrelativoPoa>(entity =>
        {
            entity.HasKey(e => e.IdCorrelativo);

            entity.ToTable("numeroCorrelativoPoa");

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
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
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
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PartidaPresupuestaria>(entity =>
        {
            entity.HasKey(e => e.IdPartida).HasName("PK__partidaP__552192F6536B0499");

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
            entity.Property(e => e.IdPrograma).HasColumnName("idPrograma");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.PartidaPresupuestaria)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK__partidaPr__idPro__3E2826D9");
        });

        modelBuilder.Entity<Planificacion>(entity =>
        {
            entity.HasKey(e => e.IdPlanificacion).HasName("PK__Planific__0D393A4205BE4C6F");

            entity.ToTable("Planificacion");

            entity.Property(e => e.IdPlanificacion).HasColumnName("idPlanificacion");
            entity.Property(e => e.CertificadoPoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("certificadoPoa");
            entity.Property(e => e.CitePlanificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("citePlanificacion");
            entity.Property(e => e.EstadoCarpeta)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("estadoCarpeta");
            entity.Property(e => e.FechaAnulacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAnulacion");
            entity.Property(e => e.FechaPlanificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPlanificacion");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");
            entity.Property(e => e.IdUnidadResponsable).HasColumnName("idUnidadResponsable");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Lugar)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lugar");
            entity.Property(e => e.MontoCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoCompra");
            entity.Property(e => e.MontoPlanificacion)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoPlanificacion");
            entity.Property(e => e.MontoPoa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoPoa");
            entity.Property(e => e.MontoPresupuesto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoPresupuesto");
            entity.Property(e => e.NombreEjecutora)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreEjecutora");
            entity.Property(e => e.NombreRegional)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreRegional");
            entity.Property(e => e.NumeroPlanificacion)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("numeroPlanificacion");
            entity.Property(e => e.ReferenciaPlanificacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenciaPlanificacion");
            entity.Property(e => e.UnidadProceso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("unidadProceso");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK__Planifica__idCen__45C948A1");

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK__Planifica__idDoc__44D52468");

            entity.HasOne(d => d.IdUnidadResponsableNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdUnidadResponsable)
                .HasConstraintName("FK__Planifica__idUni__46BD6CDA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Planificacions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Planifica__idUsu__47B19113");
        });

        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity.HasKey(e => e.IdPresupuesto);

            entity.ToTable("presupuesto");

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
            entity.Property(e => e.MontoPresupuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoPresupuesto");
            entity.Property(e => e.NroPlanificacion).HasColumnName("nroPlanificacion");
            entity.Property(e => e.Nulo).HasColumnName("nulo");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.IdPrograma).HasName("PK__Programa__467DDFD66B3A2E02");

            entity.ToTable("Programa");

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

        modelBuilder.Entity<RequerimientoPoa>(entity =>
        {
            entity.HasKey(e => e.IdRequerimientoPoa);

            entity.ToTable("requerimientoPoa");

            entity.Property(e => e.IdRequerimientoPoa).HasColumnName("idRequerimientoPoa");
            entity.Property(e => e.CiteRequerimientoPoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("citeRequerimientoPoa");
            entity.Property(e => e.EstadoRequerimientoPoa)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("estadoRequerimientoPoa");
            entity.Property(e => e.FechaAnulacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAnulacion");
            entity.Property(e => e.FechaRequerimientoPoa)
                .HasColumnType("datetime")
                .HasColumnName("fechaRequerimientoPoa");
            entity.Property(e => e.IdCentro).HasColumnName("idCentro");
            entity.Property(e => e.IdUnidadResponsable).HasColumnName("idUnidadResponsable");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Lugar)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("lugar");
            entity.Property(e => e.MontoPoa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montoPoa");
            entity.Property(e => e.NombreEjecutora)
                .HasMaxLength(75)
                .IsFixedLength()
                .HasColumnName("nombreEjecutora");
            entity.Property(e => e.NombreRegional)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombreRegional");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.RequerimientoPoas)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_requerimientoPoa_centroSalud");

            entity.HasOne(d => d.IdUnidadResponsableNavigation).WithMany(p => p.RequerimientoPoas)
                .HasForeignKey(d => d.IdUnidadResponsable)
                .HasConstraintName("FK_requerimientoPoa_unidadResponsable");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RequerimientoPoas)
                 .HasForeignKey(d => d.IdUsuario)
                 .HasConstraintName("FK_requerimientoPoa_Usuario");
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
            entity.HasKey(e => e.IdRolMenu).HasName("PK__RolMenu__CD2045D8ABA50071");

            entity.ToTable("RolMenu");

            entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__RolMenu__idMenu__4C764630");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__RolMenu__idRol__4B8221F7");
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
                .IsFixedLength()
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

        modelBuilder.Entity<UnidadProceso>(entity =>
        {
            entity.HasKey(e => e.IdUnidadproceso);

            entity.ToTable("unidadProceso");

            entity.Property(e => e.IdUnidadproceso).HasColumnName("idUnidadproceso");
            entity.Property(e => e.Abrevia)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UnidadResponsable>(entity =>
        {
            entity.HasKey(e => e.IdUnidadResponsable);

            entity.ToTable("unidadResponsable");

            entity.Property(e => e.IdUnidadResponsable).HasColumnName("idUnidadResponsable");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6F551B58C");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdUnidadResponsable).HasColumnName("idUnidadResponsable");
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
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
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
                .HasMaxLength(50)
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
                .HasConstraintName("FK__Usuario__idRol__3A5795F5");

            entity.HasOne(d => d.IdUnidadResponsableNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdUnidadResponsable)
                .HasConstraintName("FK_Usuario_unidadResponsable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
