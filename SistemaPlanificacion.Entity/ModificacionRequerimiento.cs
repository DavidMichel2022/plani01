using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class ModificacionRequerimiento
    {
        public int? IdModificacionRequerimiento { get; set; }

        public int IdModificacionPoa { get; set; }

        public int IdDetalleRequerimientoPoa { get; set; }

        //public virtual DetalleRequerimientoPoa IdDetalleRequerimientoPoaNavigation { get; set; } = null!;

        //public virtual ModificacionPoa IdModificacionPoaNavigation { get; set; } = null!;
    }
}
