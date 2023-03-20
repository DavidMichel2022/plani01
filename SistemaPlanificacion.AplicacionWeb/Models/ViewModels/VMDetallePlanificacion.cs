using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetallePlanificacion
    {
        public int? IdPartida { get; set; }
        public string? CodigoPartida { get; set; }
        public string? NombrePartida { get; set; }
        public string? ProgramaPartida { get; set; }
        public string? NombreItem { get; set; }
        public string? Medida { get; set; }
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
        public string? Total { get; set; }
        public int? IdActividad { get; set; }
        public string? NombreActividad { get; set; }
    }
}
