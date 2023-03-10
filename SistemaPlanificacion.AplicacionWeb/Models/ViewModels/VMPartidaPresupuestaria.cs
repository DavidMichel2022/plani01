using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPartidaPresupuestaria
    {
        public int IdPartida { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? Stock { get; set; }
        public int? IdPrograma { get; set; }
        public int? EsActivo { get; set; }
    }
}
