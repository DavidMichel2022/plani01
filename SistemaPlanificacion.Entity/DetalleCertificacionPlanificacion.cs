using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class DetalleCertificacionPlanificacion
    {
        public int IdCertificacionPlanificacion { get; set; }

        public int IdDetallePlanificacion { get; set; }

        public decimal? MontoPlanificacion { get; set; }

        public virtual CertificacionPlanificacion IdCertificacionPlanificacionNavigation { get; set; } = null!;

        public virtual DetallePlanificacion IdDetallePlanificacionNavigation { get; set; } = null!;
    }

}
