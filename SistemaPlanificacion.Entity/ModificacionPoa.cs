using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class ModificacionPoa
    {
        public int? IdModificacionPoa { get; set; }

        public byte? TipoAjuste { get; set; }

        public string? Lugar { get; set; }

        public string? Cite { get; set; }

        public string? Justificacion { get; set; }

        public bool? EditCantidad { get; set; }

        public bool? EditIndicador { get; set; }

        public bool? EditTemporalidad { get; set; }

        public bool? EditPrecio { get; set; }

        public decimal? TotalActual { get; set; }

        public decimal? TotalModificar { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int? IdUsuarioRegistro { get; set; }

        public int? IdUsuarioAprobacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public int? idUnidadResponsable { get; set; }

        public string? EstadoModificacion { get; set; }

        public int? Gestion { get; set; }
        public virtual ICollection<DetalleModificacion> DetalleModificacions { get; set; } = new List<DetalleModificacion>();

    }
}
