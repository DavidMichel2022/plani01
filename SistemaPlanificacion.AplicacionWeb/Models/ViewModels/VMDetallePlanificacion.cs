using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetallePlanificacion
    {
        public VMDetallePlanificacion()
        {
            PartidaPresupuestaria = new HashSet<VMPartidaPresupuestaria>();
        }

        public int? IdDetallePlanificacion { get; set; }
        public int? IdPlanificacion { get; set; }
        public int? IdPartida { get; set; }
        public string? CodigoPartida { get; set; }
        public string? NombrePartida { get; set; }
        public string? ProgramaPartida { get; set; }
        public string? NombreItem { get; set; }
        public string? Medida { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
        public int? CodigoActividad { get; set; }
        public string? Temporalidad { get; set; }
        public string? Observacion { get; set; }
        public virtual ICollection<VMPartidaPresupuestaria> PartidaPresupuestaria { get; set; }
        //public virtual PartidaPresupuestaria? IdPartidaNavigation { get; set; }
    }
}
