using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class TipoDocumento
{
    public int IdDocumento { get; set; }
    public string? Codigo { get; set; }
    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Planificacion> Planificacions { get; } = new List<Planificacion>();
}
