using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPlanificacion
    {
        public int IdPlanificacion { get; set; }

        public string? CitePlanificacion { get; set; }
        public string? Lugar { get; set; }

        public int? IdDocumento { get; set; }
        public string? NombreDocumento { get; set; }

        public DateTime? FechaPlanificacion { get; set; }

        public int? IdActividad { get; set; }
        public string? NombreActividad { get; set; }

        public int? IdPrograma { get; set; }
        public string? NombrePrograma { get; set; }

        public int? IdCentro { get; set; }
        public string? NombreCentro { get; set; }

        public int? IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public int? IdResponsable { get; set; }

        public string? MontoPoa { get; set; }
        public string? MontoPlanificacion { get; set; }
        public string? MontoPresupuesto { get; set; }
        public string? MontoCompra { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int? IdUnidadProceso { get; set; }
        public string? NombreUnidadProceso { get; set; }

        public virtual ICollection<VMDetallePlanificacion> DetallePlanificacion { get; set; }
    }
}
