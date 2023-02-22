using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int IdPlanificacion { get; set; }

    public int? NroPlanificacion { get; set; }

    public string? CuceCompra { get; set; }

    public string? ObjContratocompra { get; set; }

    public string? ModalidadCompra { get; set; }

    public DateTime? FechaCompra { get; set; }

    public string? NrofacturaCompra { get; set; }

    public float? MontoadjudicadoCompra { get; set; }

    public DateTime? FechabienContratado { get; set; }

    public bool? Nulo { get; set; }

    public virtual Planificacion IdPlanificacionNavigation { get; set; } = null!;
}
