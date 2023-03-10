using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMNumeroCorrelativo
    {
        public int IdCorrelativo { get; set; }

        public int? Ultimonumero { get; set; }

        public int? CantidadDigitos { get; set; }

        public string? Gestion { get; set; }
    }
}
