using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class NumeroCorrelativo
{
    public int IdCorrelativo { get; set; }

    public int? UltimonroCorrelativo { get; set; }

    public int? CantidadDigitos { get; set; }

    public string? Gestion { get; set; }

    public DateTime? FechaActualizacion { get; set; }
}
