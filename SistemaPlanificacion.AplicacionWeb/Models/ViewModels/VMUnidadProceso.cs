using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMUnidadProceso
    {
        public int IdUnidadProceso { get; set; }
        public string? Nombre { get; set; }
        public string? Abrevia { get; set; }
        public int? EsActivo { get; set; }
    }
}
