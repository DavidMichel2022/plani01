using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DocmCompra
{
    public int IdCompra { get; set; }

    public int? IdPlanificacion { get; set; }

    public string? CuceCompra { get; set; }

    public string? ObjcontratoCompra { get; set; }

    public string? ModalidadCompra { get; set; }

    public DateTime? FechaCompra { get; set; }

    public string? NrofacturaCompra { get; set; }

    public float? MontoadjudicadoCompra { get; set; }

    public DateTime? FechacontratoCompra { get; set; }

    public bool? Nulo { get; set; }

    public virtual DocmPlanificacion? IdPlanificacionNavigation { get; set; }
}
