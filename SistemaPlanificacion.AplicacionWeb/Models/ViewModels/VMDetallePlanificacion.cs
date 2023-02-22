using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetallePlanificacion
    {
        public int IdPlanificacion { get; set; }
        public int? IdPartida { get; set; }

        public string? NombrePartida { get; set; }

        public string? NombreitemPlanificacion { get; set; }

        public string? MontoPoa { get; set; }

        public string? MontoPlanificacion { get; set; }

        public string? MontoPresupuesto { get; set; }

        public string? MontoCompra { get; set; }

        public int? IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public int? Nulo { get; set; }
    }
}
