using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class RequerimientoPoa
    {
        public RequerimientoPoa()
        {
            DetalleRequerimientoPoas = new HashSet<DetalleRequerimientoPoa>();
        }
        public int IdRequerimientoPoa { get; set; }

        public int? IdUnidadResponsable { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdCentro { get; set; }

        public DateTime? FechaRequerimientoPoa { get; set; }

        public string? CiteRequerimientoPoa { get; set; }

        public decimal? MontoPoa { get; set; }

        public string? EstadoRequerimientoPoa { get; set; }

        public DateTime? FechaAnulacion { get; set; }

        public string? Lugar { get; set; }

        public string? NombreRegional { get; set; }

        public string? NombreEjecutora { get; set; }
//        public string? NumeroRequerimientoPoa { get; set; } removido
        public int? IdDocumento { get; set; }

        public virtual ICollection<DetalleRequerimientoPoa> DetalleRequerimientoPoas { get; set; }

        public virtual CentroSalud? IdCentroNavigation { get; set; }

        public virtual TipoDocumento? IdDocumentoNavigation { get; set; }

        public virtual UnidadResponsable? IdUnidadResponsableNavigation { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }

        /*  public virtual CentroSalud? IdCentroNavigation { get; set; }

          public virtual TipoDocumento? IdDocumentoNavigation { get; set; }

          public virtual UnidadResponsable? IdUnidadResponsableNavigation { get; set; }

          public virtual Usuario? IdUsuarioNavigation { get; set; }*/
    }
}
