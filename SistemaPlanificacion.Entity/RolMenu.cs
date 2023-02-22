using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class RolMenu
{
    public int IdrolMenu { get; set; }

    public int? IdRol { get; set; }

    public int? IdMenu { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
