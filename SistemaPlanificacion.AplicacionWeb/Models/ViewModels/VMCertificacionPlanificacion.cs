using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMCertificacionPlanificacion
    {
        public int IdCertificacionPlanificacion { get; set; }

        public int IdPlanificacion { get; set; }

        public string? CodigoPlanificacion { get; set; }

        public decimal? TotalCertificado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public int? IdUsuario { get; set; }

        public virtual ICollection<DetalleCertificacionPlanificacion> DetalleCertificacionPlanificacions { get; set; }

      //  public virtual Planificacion IdPlanificacionNavigation { get; set; } = null!;
    }
}
