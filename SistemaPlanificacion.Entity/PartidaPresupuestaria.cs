using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class PartidaPresupuestaria
{
    public int IdPartida { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? Stock { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; } = new List<DetallePlanificacion>();

    public virtual ICollection<MoviPlanificacion> MoviPlanificacions { get; } = new List<MoviPlanificacion>();
}
