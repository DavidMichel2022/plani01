using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMReportePlanificacion
    {
        public string? FechaRegistro { get; set; }
        public string? NumeroPlanificacion { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NombreTipoDocumento { get; set; }
        public int IdCentro { get; set; }
        public string? NombreCentro { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? MontoPoa { get; set; }
        public decimal? MontoPlanificacion { get; set; }
        public decimal? MontoPresupuesto { get; set; }
        public decimal? MontoCompra { get; set; }
    }
}
