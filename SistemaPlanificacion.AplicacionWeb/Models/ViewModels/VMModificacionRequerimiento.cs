using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMModificacionRequerimiento
    {
        public int? IdModificacionRequerimiento { get; set; }

        public int IdModificacionPoa { get; set; }

        public int IdDetalleRequerimientoPoa { get; set; }

        public virtual VMDetalleRequerimientoPoa IdDetalleRequerimientoPoaNavigation { get; set; } = null!;

        public virtual VMModificacionPoa IdModificacionPoaNavigation { get; set; } = null!;
    }
}
