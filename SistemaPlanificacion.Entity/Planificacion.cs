using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Planificacion
{
    public Planificacion()
    {
        DetallePlanificacions = new HashSet<DetallePlanificacion>();
    }
    public int IdPlanificacion { get; set; }

    public string? CitePlanificacion { get; set; }

    public string? NumeroPlanificacion { get; set; }

    public int? IdDocumento { get; set; }

    public int? IdCentro { get; set; }

    public int? IdUnidadResponsable { get; set; }

    public int? IdUsuario { get; set; }

    public string? Lugar { get; set; }

    public string? CertificadoPoa { get; set; }

    public string? ReferenciaPlanificacion { get; set; }

    public string? NombreRegional { get; set; }

    public string? NombreEjecutora { get; set; }

    public decimal? MontoPlanificacion { get; set; }

    public decimal? MontoPoa { get; set; }

    public decimal? MontoPresupuesto { get; set; }

    public decimal? MontoCompra { get; set; }

    public string? UnidadProceso { get; set; }

    public string? EstadoCarpeta { get; set; }

    public DateTime? FechaPlanificacion { get; set; }
    public DateTime? FechaAnulacion { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; set; }

    public virtual CentroSalud? IdCentroNavigation { get; set; }

    public virtual TipoDocumento? IdDocumentoNavigation { get; set; }

    public virtual UnidadResponsable? IdUnidadResponsableNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
