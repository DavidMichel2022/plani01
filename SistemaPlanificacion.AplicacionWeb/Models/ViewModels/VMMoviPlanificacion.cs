using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMMoviPlanificacion
    {
        public int IdPlanificacion { get; set; }

        public int? IdPartida { get; set; }

        public string? NombreitemPartida { get; set; }

        public decimal? MontopoaPartida { get; set; }

        public decimal? MontoplanificacionPartida { get; set; }

        public decimal? MontopresupuestoPartida { get; set; }

        public decimal? MontocompraPartida { get; set; }

        public int? IdEmpresa { get; set; }

        public int? Nulo { get; set; }
    }
}
