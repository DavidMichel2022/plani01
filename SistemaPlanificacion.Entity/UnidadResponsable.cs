using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class UnidadResponsable
{
    public UnidadResponsable()
    {
        Planificacions = new HashSet<Planificacion>();
        RequerimientoPoas = new HashSet<RequerimientoPoa>();
    }
    public int IdUnidadResponsable { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Planificacion> Planificacions { get; set; }
    public virtual ICollection<RequerimientoPoa> RequerimientoPoas { get; set; }
}
