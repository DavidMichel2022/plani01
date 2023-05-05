using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DetalleCertificacionPlanificacion
{
    public int IdCertificacionPlanificacion { get; set; }

    public int IdDetallePlanificacion { get; set; }

    public decimal? MontoPlanificacion { get; set; }

    //public virtual CertificacionPlanificacion IdCertificacionPlanificacionNavigation { get; set; } = null!;
    public virtual CertificacionPlanificacion IdPlanificacionNavigations { get; set; } = null!;
    public virtual DetallePlanificacion IdDetallePlanificacionNavigation { get; set; } = null!;
}
