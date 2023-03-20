using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Objetivo
{
    public int IdObjetivo { get; set; }

    public string? Codigo { get; set; }

    public byte[]? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
