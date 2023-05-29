using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleModificacion
    {
        public int IdDetalleRequerimientoPoa { get; set; }

        public int IdModificacionPoa { get; set; }

        public int NroOrden { get; set; }

        public string? Estado { get; set; }

        public virtual VMDetalleRequerimientoPoa IdDetalleRequerimientoPoaNavigation { get; set; } = null!;

        public virtual VMModificacionPoa IdModificacionPoaNavigation { get; set; } = null!;
    }
}
