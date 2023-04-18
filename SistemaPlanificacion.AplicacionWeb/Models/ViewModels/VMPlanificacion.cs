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
        public string? Lugar { get; set; }
        public string? NombreRegional { get; set; }
        public string? NombreEjecutora { get; set; }
        public string? MontoPlanificacion { get; set; }
        public decimal? MontoPoa { get; set; }
        public decimal? MontoPresupuesto { get; set; }
        public decimal? MontoCompra { get; set; }
        public string? ReferenciaPlanificacion { get; set; }
        public string? UnidadProceso { get; set; }
        public string? CertificadoPoa { get; set; }
        public string? EstadoCarpeta { get; set; }
        public string? FechaPlanificacion { get; set; }
        public DateTime? FechaAnulacion { get; set; }

        public virtual ICollection<VMDetallePlanificacion> DetallePlanificacion { get; set; }
    }
}
