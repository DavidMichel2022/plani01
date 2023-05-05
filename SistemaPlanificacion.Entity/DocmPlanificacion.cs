using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DocmPlanificacion
{
    public int IdPlanificacion { get; set; }

    public DateTime? FechaPlanificacion { get; set; }

    public string? CertificadopoaPlanificacion { get; set; }

    public string? ReferenciaPlanificacion { get; set; }

    public int? IdEmpresa { get; set; }

    public int IdPrograma { get; set; }

    public int? IdActividad { get; set; }

    public int? IdCentro { get; set; }

    public decimal? MontopoaPlanificacion { get; set; }

    public decimal? MontoPlanificacion { get; set; }

    public string? UbicacionPlanificacion { get; set; }

    public int? IdUsuario { get; set; }

    public bool? Nulo { get; set; }
}
