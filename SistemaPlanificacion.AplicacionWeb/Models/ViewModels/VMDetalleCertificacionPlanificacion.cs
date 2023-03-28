using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleCertificacionPlanificacion
    {
        public int IdCertificacionPlanificacion { get; set; }

        public int IdDetallePlanificacion { get; set; }

        public decimal? MontoPlanificacion { get; set; }

        //public virtual CertificacionPlanificacion IdCertificacionPlanificacionNavigation { get; set; } = null!;

      //  public virtual DetallePlanificacion IdDetallePlanificacionNavigation { get; set; } = null!;
    }
}
