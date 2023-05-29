using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class DetalleModificacion
    {
        public int IdDetalleRequerimientoPoa { get; set; }

        public int IdModificacionPoa { get; set; }

        public int NroOrden { get; set; }

        public string? Estado { get; set; }

        public virtual DetalleRequerimientoPoa IdDetalleRequerimientoPoaNavigation { get; set; } = null!;

        public virtual ModificacionPoa IdModificacionPoaNavigation { get; set; } = null!;
    }

}
