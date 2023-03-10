using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDocmPlanificacion
    {
        public int IdPlanificacion { get; set; }

        public DateTime? FechaPlanificacion { get; set; }

        public string? CertificadopoaPlanificacion { get; set; }

        public string? ReferenciaPlanificacion { get; set; }

        public int? IdPrograma { get; set; }

        public int? IdActividad { get; set; }

        public int? IdCentro { get; set; }

        public decimal? MontopoaPlanificacion { get; set; }

        public decimal? MontoPlanificacion { get; set; }

        public string? UbicacionPlanificacion { get; set; }

        public int? IdusuarioPlanificacion { get; set; }

        public int? Nulo { get; set; }
    }
}
