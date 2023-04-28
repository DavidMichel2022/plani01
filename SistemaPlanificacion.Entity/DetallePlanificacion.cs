using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DetallePlanificacion
{
    public int? IdDetallePlanificacion { get; set; }

    public int? IdPlanificacion { get; set; }

    public int? IdPartida { get; set; }

    public string? NombreItem { get; set; }

    public string? Medida { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public string? Temporalidad { get; set; }

    public string? Observacion { get; set; }

    public int? CodigoActividad { get; set; }
    public decimal? Mes_Ene { get; set; }
    public decimal? Mes_Feb { get; set; }
    public decimal? Mes_Mar { get; set; }
    public decimal? Mes_Abr { get; set; }
    public decimal? Mes_May { get; set; }
    public decimal? Mes_Jun { get; set; }
    public decimal? Mes_Jul { get; set; }
    public decimal? Mes_Ago { get; set; }
    public decimal? Mes_Sep { get; set; }
    public decimal? Mes_Oct { get; set; }
    public decimal? Mes_Nov { get; set; }
    public decimal? Mes_Dic { get; set; }
    public virtual PartidaPresupuestaria? IdPartidaNavigation { get; set; }

    public virtual Planificacion? IdPlanificacionNavigation { get; set; }
}
