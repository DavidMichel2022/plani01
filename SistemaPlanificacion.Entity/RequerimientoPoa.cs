using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class RequerimientoPoa
{
    public int IdRequerimientoPoa { get; set; }

    public string? CiteRequerimientoPoa { get; set; }

    public string? NumeroRequerimientoPoa { get; set; }

    public int? IdCentro { get; set; }

    public int? IdUsuario { get; set; }

    public decimal? MontoPoa { get; set; }

    public string? EstadoCarpeta { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaAnulacion { get; set; }

    public virtual ICollection<DetalleRequerimientoPoa> DetalleRequerimientoPoas { get; } = new List<DetalleRequerimientoPoa>();

    public virtual CentroSalud? IdCentroNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
