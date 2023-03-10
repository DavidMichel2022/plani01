using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Rol
{
    public Rol()
    {
        RolMenu = new HashSet<RolMenu>();
        Usuario=new HashSet<Usuario>();
    }
    public int IdRol { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }
    public virtual ICollection<RolMenu> RolMenu { get; } = new List<RolMenu>();
    public virtual ICollection<Usuario> Usuario { get; } = new List<Usuario>();
}
