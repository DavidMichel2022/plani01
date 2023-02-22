using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMMoviPlanificacion
    {
        public int IdPlanificacion { get; set; }

        public int? IdPartida { get; set; }

        public string? NombreitemPartida { get; set; }

        public string? MontopoaPartida { get; set; }

        public string? MontoplanificacionPartida { get; set; }

        public string? MontopresupuestoPartida { get; set; }

        public string? MontocompraPartida { get; set; }

        public int? IdEmpresa { get; set; }

        public int? Nulo { get; set; }
    }
}
