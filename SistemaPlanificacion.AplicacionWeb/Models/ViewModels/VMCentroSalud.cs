using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMCentroSalud
    {
        public int IdCentro { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? EsActivo { get; set; }
    }
}
