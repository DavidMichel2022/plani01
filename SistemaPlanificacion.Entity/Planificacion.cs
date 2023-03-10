using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Planificacion
{
    public Planificacion()
    {
        DetallePlanificacion = new HashSet<DetallePlanificacion>();
    }
    public int IdPlanificacion { get; set; }

    public string? NumeroPlanificacion { get; set; }

    public int? IdDocumento { get; set; }
    public string? Lugar { get; set; }

    public DateTime? FechaPlanificacion { get; set; }
    public string? CitePlanificacion { get; set; }
    public string? CertificadoPoa { get; set; }

    public string? ReferenciaPlanificacion { get; set; }
    public string? NombreRegional { get; set; }
    public string? NombreEjecutora { get; set; }

    public int? IdActividad { get; set; }
    public int? IdPrograma { get; set; }

    public int? IdCentro { get; set; }

    public int? IdEmpresa { get; set; }
    public int? IdResponsable { get; set; }

    public decimal? MontoPlanificacion { get; set; }
    public decimal? MontoPoa { get; set; }
    public decimal? MontoPresupuesto { get; set; }
    public decimal? MontoCompra { get; set; }

    public int? IdUsuario { get; set; }
    public int? IdunidadProceso { get; set; }


    public string? UbicacionPlanificacion { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacion { get; set; }

    public virtual Actividad? IdPlanificacionActividadNavigation { get; set; }
    public virtual TipoDocumento? IdPlanificacionTipoDocumentoNavigation { get; set; }

    public virtual CentroSalud? IdPlanificacionCentroNavigation { get; set; }
    public virtual ICollection<Compra> Compra { get; set; }
    public virtual Empresa? IdPlanificacionEmpresaNavigation { get; set; }

    public virtual Programa? IdPlanificacionProgramaNavigation { get; set; }

    public virtual Usuario? IdPlanificacionUsuarioNavigation { get; set; }
    public virtual UnidadProceso? IdPlanificacionUnidadProcesoNavigation { get; set; }

    public virtual ICollection<Presupuesto> Presupuesto { get; set; }
}
