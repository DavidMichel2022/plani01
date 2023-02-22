using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMMenu
    {
        public int IdMenu { get; set; }

        public string? Descripcion { get; set; }

        public int? IdmenuPadre { get; set; }

        public string? Icono { get; set; }

        public string? Controlador { get; set; }

        public string? PaginaAccion { get; set; }

        public int? EsActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public virtual ICollection<VMMenu> SubMenus { get; set; }
        public virtual Menu? IdmenuPadreNavigation { get; set; }
    }
}
