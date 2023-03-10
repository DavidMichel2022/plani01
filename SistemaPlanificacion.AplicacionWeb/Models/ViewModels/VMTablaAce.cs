using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMTablaAce
    {
        public int IdAce { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }

        public int? EsActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
