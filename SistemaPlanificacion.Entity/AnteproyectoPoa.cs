using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class AnteproyectoPoa
    {
        public AnteproyectoPoa()
        {
            DetalleAnteproyectoPoas = new HashSet<DetalleAnteproyectoPoa>();
        }
        public int IdAnteproyecto { get; set; }

        public int? IdUnidadResponsable { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdCentro { get; set; }

        public DateTime? FechaAnteproyecto { get; set; }

        public string? CiteAnteproyecto { get; set; }

        public decimal? MontoAnteproyecto { get; set; }

        public string? EstadoAnteproyecto { get; set; }

        public DateTime? FechaAnulacion { get; set; }

        public string? Lugar { get; set; }

        public string? NombreRegional { get; set; }

        public string? NombreEjecutora { get; set; }
        public virtual ICollection<DetalleAnteproyectoPoa> DetalleAnteproyectoPoas { get; set; }

        public virtual CentroSalud? IdCentroNavigation { get; set; }

        public virtual UnidadResponsable? IdUnidadResponsableNavigation { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
