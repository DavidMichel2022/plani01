﻿using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class TablaAce
{
    public int IdAce { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
