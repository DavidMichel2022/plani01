using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Actividad
{
    public Actividad()
    {
        PlanificacionPrograma = new HashSet<Planificacion>();
    }

    public int IdActividad { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
    public string? CodigoUnidad { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacion { get; } = new List<DocmPlanificacion>();

    public virtual ICollection<Planificacion> Planificacion { get; } = new List<Planificacion>();

    public virtual ICollection<Presupuesto> Presupuesto { get; } = new List<Presupuesto>();

    public virtual ICollection<Planificacion> PlanificacionPrograma { get; } = new List<Planificacion>();

}
