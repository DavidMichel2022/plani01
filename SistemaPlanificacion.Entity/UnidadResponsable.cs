using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class UnidadResponsable
{
    public UnidadResponsable()
    {
        Planificacions = new HashSet<Planificacion>();
        RequerimientoPoas = new HashSet<RequerimientoPoa>();
        AnteproyectoPoas = new HashSet<AnteproyectoPoa>();
        Usuarios = new HashSet<Usuario>();
    }
    public int IdUnidadResponsable { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Planificacion> Planificacions { get; set; }
    public virtual ICollection<RequerimientoPoa> RequerimientoPoas { get; set; }
    public virtual ICollection<AnteproyectoPoa> AnteproyectoPoas { get; set; }
    public virtual ICollection<Usuario> Usuarios { get; set; }
}
