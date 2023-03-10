using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DetallePlanificacion
{
    public int IdDetallePlanificacion { get; set; }

    public int? IdPlanificacion { get; set; }

    public string? NumeroPlanificacion { get; set; }

    public int? IdPartida { get; set; }

    public string? NombreItem { get; set; }
    public string? Medida { get; set; }
    public decimal? Cantidad { get; set; }
    public decimal? Precio { get; set; }
    public decimal? Total { get; set; }
    public int? IdActividad { get; set; }
    public bool? Nulo { get; set; }

    public virtual PartidaPresupuestaria? IdPartidaNavigation { get; set; }
    public virtual PartidaPresupuestaria? IdDetallePlanificacionPartidaNavigation { get; set; }
    public virtual Actividad? IdDetallePlanificacionActividadNavigation { get; set; }
    public virtual Planificacion? IdDetallePlanificacionPlanificacionNavigation { get; set; }
    public virtual Planificacion? IdPlanificacionNavigation { get; set; }
}
