using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DocmPresupuesto
{
    public int IdPresupuesto { get; set; }

    public int? IdPlanificacion { get; set; }

    public string? CertPresupuesto { get; set; }

    public int? IdPrograma { get; set; }

    public int? IdActividad { get; set; }

    public DateTime? FechaPresupuesto { get; set; }

    public decimal? MontoPresupuesto { get; set; }

    public bool? Nulo { get; set; }
}
