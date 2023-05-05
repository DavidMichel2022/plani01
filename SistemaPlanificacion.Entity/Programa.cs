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

    public virtual ICollection<PartidaPresupuestaria> PartidaPresupuestaria { get; } = new List<PartidaPresupuestaria>();
}
