using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class PartidaPresupuestaria
{
    public PartidaPresupuestaria()
    {
        DetallePlanificacions = new HashSet<DetallePlanificacion>();
        DetalleRequerimientoPoas = new HashSet<DetalleRequerimientoPoa>();
    }
    public int IdPartida { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? IdPrograma { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; set; }
    public virtual ICollection<DetalleRequerimientoPoa> DetalleRequerimientoPoas { get; set; }

    public virtual Programa? IdProgramaNavigation { get; set; }
}
