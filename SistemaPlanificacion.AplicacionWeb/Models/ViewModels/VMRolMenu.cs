using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMRolMenu
    {
        public int IdrolMenu { get; set; }

        public int? IdRol { get; set; }

        public int? IdMenu { get; set; }
        public int? IdMenuPadre { get; set; }
        public int? EsActivo { get; set; }

    }
}
