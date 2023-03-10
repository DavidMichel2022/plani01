using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class MoviPlanificacion
{
    public int IdMoviPlanificacion { get; set; }

    public int IdPlanificacion { get; set; }

    public int? IdPartida { get; set; }

    public string? NombreitemPartida { get; set; }

    public decimal? MontopoaPartida { get; set; }

    public decimal? MontoplanificacionPartida { get; set; }

    public decimal? MontopresupuestoPartida { get; set; }

    public decimal? MontocompraPartida { get; set; }

    public bool? Nulo { get; set; }

    public virtual PartidaPresupuestaria? IdPartidaNavigation { get; set; }

    public virtual DocmPlanificacion IdPlanificacionNavigation { get; set; } = null!;
}
