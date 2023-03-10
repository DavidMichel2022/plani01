using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class CentroSalud
{
    public CentroSalud()
    {
        PlanificacionCentroSalud = new HashSet<Planificacion>();
    }

    public int IdCentro { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacion { get; } = new List<DocmPlanificacion>();

    public virtual ICollection<Planificacion> Planificacion { get; } = new List<Planificacion>();

    public virtual ICollection<Planificacion> PlanificacionCentroSalud { get; } = new List<Planificacion>();

}
