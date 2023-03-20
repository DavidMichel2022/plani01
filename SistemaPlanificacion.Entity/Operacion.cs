using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Operacion
{
    public int IdOperacion { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
