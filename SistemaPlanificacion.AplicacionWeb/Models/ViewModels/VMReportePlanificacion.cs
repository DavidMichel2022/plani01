using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMReportePlanificacion
    {
        public string? FechaPlanificacion { get; set; }
        public string? NumeroPlanificacion { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NombreTipoDocumento { get; set; }
        public string? CitePlanificacion { get; set; }
        public int IdCentro { get; set; }
        public string? NombreCentro { get; set; }
        public string? NombreUnidadResponsable { get; set; }
        public decimal? MontoPlanificacion { get; set; }
        public string? NombrePartida { get; set; }
        public string? NombreItem { get; set; }
        public string? Medida { get; set; }
        public string? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Total { get; set; }
        public string? CodigoActividad { get; set; }
    }
}
