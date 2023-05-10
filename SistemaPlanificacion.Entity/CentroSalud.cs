using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class CentroSalud
{
    public CentroSalud()
    {
        Planificacions = new HashSet<Planificacion>();
        RequerimientoPoas = new HashSet<RequerimientoPoa>();
    }
    public int IdCentro { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Planificacion> Planificacions { get; set; }
    public virtual ICollection<RequerimientoPoa> RequerimientoPoas { get; set; }
}
