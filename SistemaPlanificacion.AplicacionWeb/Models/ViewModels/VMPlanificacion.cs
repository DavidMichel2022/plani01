using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPlanificacion
    {
        public VMPlanificacion()
        {
            DetallePlanificacion = new HashSet<VMDetallePlanificacion>();
        }

        public int IdPlanificacion { get; set; }

        public string? NumeroPlanificacion { get; set; }
        public int? IdDocumento { get; set; }
        public string? NombreDocumento { get; set; }
        public int? IdCentro { get; set; }
        public string? NombreCentro { get; set; }
        public int? IdUnidadResponsable { get; set; }
        public string? NombreUnidadResponsable { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CitePlanificacion { get; set; }
        public string? NombreRegional { get; set; }
        public string? NombreEjecutora { get; set; }
        public decimal? MontoPlanificacion { get; set; }
        public DateTime? FechaPlanificacion { get; set; }
        public virtual ICollection<VMDetallePlanificacion> DetallePlanificacion { get; set; }
    }
}
