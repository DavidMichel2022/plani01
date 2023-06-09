namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMReporteRequerimientoPoa
    {
       
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

    
        public int IdDetalleRequerimientoPoa { get; set; }

        public int? IdPartida { get; set; }
        public string? CodigoPartida { get; set; }
        public string? NombrePartida { get; set; }
        public string? ProgramaPartida { get; set; }

        public string? Detalle { get; set; }

        public string? Medida { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }

        public decimal? MesEne { get; set; }
        public decimal? MesFeb { get; set; }
        public decimal? MesMar { get; set; }
        public decimal? MesAbr { get; set; }
        public decimal? MesMay { get; set; }
        public decimal? MesJun { get; set; }
        public decimal? MesJul { get; set; }
        public decimal? MesAgo { get; set; }
        public decimal? MesSep { get; set; }
        public decimal? MesOct { get; set; }
        public decimal? MesNov { get; set; }
        public decimal? MesDic { get; set; }
        public string? Observacion { get; set; }
        public int? CodigoActividad { get; set; }

        public string? Estado { get; set; }
    }
}
