using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class PartidaPresupuestaria
{
    public PartidaPresupuestaria()
    {
        DetallePlanificacion = new HashSet<DetallePlanificacion>();
    }

    public int IdPartida { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }
    public decimal? Stock { get; set; }
    public decimal? Precio { get; set; }
    public bool? EsActivo { get; set; }
    public int IdPrograma { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacion { get; set; }
}
