using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMRequerimientoPoa
    {
        public VMRequerimientoPoa()
        {
            DetalleRequerimientoPoa = new HashSet<VMDetalleRequerimientoPoa>();
        }
        public int IdRequerimientoPoa { get; set; }

        public int? IdUnidadResponsable { get; set; }
        public string? NombreUnidadResponsable { get; set; }

        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int? IdCentro { get; set; }
        public string? NombreCentro { get; set; }

        public DateTime? FechaRequerimientoPoa { get; set; }

        public string? CiteRequerimientoPoa { get; set; }

        public decimal? MontoPoa { get; set; }

        public string? EstadoRequerimientoPoa { get; set; }

        public DateTime? FechaAnulacion { get; set; }

        public string? Lugar { get; set; }

        public string? NombreRegional { get; set; }

        public string? NombreEjecutora { get; set; }
        public int? IdDocumento { get; set; }
        public string? NombreDocumento { get; set; }

        // public string? NumeroRequerimientoPoa { get; set; }
        // public int? IdDocumento { get; set; }
        //public string? NombreDocumento { get; set; }

        public virtual ICollection<VMDetalleRequerimientoPoa> DetalleRequerimientoPoa { get; set; }
    }
}
