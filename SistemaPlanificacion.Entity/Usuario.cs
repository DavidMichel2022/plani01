using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Usuario
{
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

    public virtual ICollection<DocmPlanificacion> DocmPlanificacions { get; } = new List<DocmPlanificacion>();

    public virtual Rol? IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Planificacion> Planificacions { get; } = new List<Planificacion>();
}
