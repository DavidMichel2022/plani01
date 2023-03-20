using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class UnidadProceso
{
    public int IdUnidadproceso { get; set; }

    public string? Nombre { get; set; }

    public string? Abrevia { get; set; }

    public bool? EsActivo { get; set; }
}
