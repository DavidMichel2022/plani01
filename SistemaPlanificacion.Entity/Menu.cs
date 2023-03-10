﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace SistemaPlanificacion.Entity;

public partial class Menu
{
    public Menu()
    {
        InverseIdmenuPadreNavigation = new HashSet<Menu>();
        RolMenu = new HashSet<RolMenu>();
    }
    public int IdMenu { get; set; }

    public string? Descripcion { get; set; }

    public int? IdmenuPadre { get; set; }

    public string? Icono { get; set; }

    public string? Controlador { get; set; }

    public string? PaginaAccion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Menu? IdmenuPadreNavigation { get; set; }
    public virtual ICollection<RolMenu> RolMenu { get; set; }

    public virtual ICollection<Menu> InverseIdmenuPadreNavigation { get; set; }
}
