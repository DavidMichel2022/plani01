using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class CertificacionPlanificacion
    {
        public int IdCertificacionPlanificacion { get; set; }

        public int IdPlanificacion { get; set; }

        public string? CodigoPlanificacion { get; set; }

        public decimal? TotalCertificado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public int? IdUsuario { get; set; }

        public virtual ICollection<DetalleCertificacionPlanificacion> DetalleCertificacionPlanificacions { get; set; } = new List<DetalleCertificacionPlanificacion>();

       // public virtual Planificacion IdPlanificacionNavigation { get; set; } = null!;
    }
}
