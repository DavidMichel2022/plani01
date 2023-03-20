using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Actividad
{
    public Actividad()
    {
        DetallePlanificacions = new HashSet<DetallePlanificacion>();
    }
    public int IdActividad { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? CodigoUnidad { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; set; }
}
