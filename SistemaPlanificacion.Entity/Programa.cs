using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Programa
{
    public int IdPrograma { get; set; }

    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacions { get; } = new List<DocmPlanificacion>();

    public virtual ICollection<DocmPresupuesto> DocmPresupuestos { get; } = new List<DocmPresupuesto>();

    public virtual ICollection<Planificacion> Planificacions { get; } = new List<Planificacion>();

    public virtual ICollection<Presupuesto> Presupuestos { get; } = new List<Presupuesto>();
}
