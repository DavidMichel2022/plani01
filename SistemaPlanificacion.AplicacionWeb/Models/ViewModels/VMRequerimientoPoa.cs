using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMRequerimientoPoa
    {
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

        public virtual ICollection<VMDetalleRequerimientoPoa> DetalleRequerimientoPoas { get; set; } = new List<VMDetalleRequerimientoPoa>();

        //public virtual CentroSalud? IdCentroNavigation { get; set; }

        //public virtual UnidadResponsable? IdUnidadResponsableNavigation { get; set; }

        //public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
