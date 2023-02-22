using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class RolGeneral
{
    public int IdRol { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
