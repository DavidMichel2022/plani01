using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Usuario
{
    public Usuario()
    {
        PlanificacionUsuario = new HashSet<Planificacion>();
    }
    public int IdUsuario { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public int IdRol { get; set; }

    public string? UrlFoto { get; set; }

    public string? NombreFoto { get; set; }

    public string? Clave { get; set; }

    public string? Carnet { get; set; }

    public string? Cargo { get; set; }

    public string? Profesion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DocmPlanificacion> DocmPlanificacion { get; set; }

    public virtual Rol? IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Planificacion> Planificacion { get; set; }

    public virtual ICollection<Planificacion> PlanificacionUsuario { get; set; }
}
