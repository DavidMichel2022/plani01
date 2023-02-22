using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Empresa
{
    public int IdEmpresa { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacions { get; } = new List<DocmPlanificacion>();

    public virtual ICollection<Planificacion> Planificacions { get; } = new List<Planificacion>();
}
