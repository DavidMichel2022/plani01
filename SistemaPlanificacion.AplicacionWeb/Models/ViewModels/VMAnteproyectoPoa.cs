using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMAnteproyectoPoa
    {
        public VMAnteproyectoPoa()
        {
            DetalleAnteproyectoPoas = new HashSet<VMDetalleAnteproyectoPoa>();
        }
        public int IdAnteproyecto { get; set; }

        public int? IdUnidadResponsable { get; set; }
        public string? NombreUnidadResponsable { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int? IdCentro { get; set; }
        public string? NombreCentro { get; set; }

        public string? FechaAnteproyecto { get; set; }

        public string? CiteAnteproyecto { get; set; }

        public decimal? MontoAnteproyecto { get; set; }

        public string? EstadoAnteproyecto { get; set; }

        public DateTime? FechaAnulacion { get; set; }

        public string? Lugar { get; set; }

        public string? NombreRegional { get; set; }

        public string? NombreEjecutora { get; set; }

        public virtual ICollection<VMDetalleAnteproyectoPoa> DetalleAnteproyectoPoas { get; set; }
    }
}
