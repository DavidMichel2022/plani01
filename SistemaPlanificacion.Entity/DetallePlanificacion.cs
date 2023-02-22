using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DetallePlanificacion
{
    public int IdDetallePlanificacion { get; set; }

    public int? IdPlanificacion { get; set; }

    public int? NroPlanificacion { get; set; }

    public int? IdPartida { get; set; }

    public string? NombreitemPlanificacion { get; set; }

    public float? MontoPoa { get; set; }

    public float? MontoPlanificacion { get; set; }

    public float? MontoPresupuesto { get; set; }

    public float? MontoCompra { get; set; }

    public bool? Nulo { get; set; }

    public virtual PartidaPresupuestaria? IdPartidaNavigation { get; set; }

    public virtual Planificacion? IdPlanificacionNavigation { get; set; }
}
