using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Empresa
{
    public Empresa()
    {
        PlanificacionEmpresa = new HashSet<Planificacion>();
    }

    public int IdEmpresa { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacion { get; } = new List<DocmPlanificacion>();

    public virtual ICollection<Planificacion> Planificacion { get; } = new List<Planificacion>();

    public virtual ICollection<Planificacion> PlanificacionEmpresa { get; } = new List<Planificacion>();

}
