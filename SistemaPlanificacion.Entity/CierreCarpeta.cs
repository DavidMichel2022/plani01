using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class CierreCarpeta
{
    public int IdPlanificacion { get; set; }

    public int? NroPlanificacion { get; set; }

    public string? PartidaPresupuesto { get; set; }

    public float? SaldoFinal { get; set; }

    public string? TipoCierre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Nulo { get; set; }
}
