using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class DetalleModificacion
    {
        public int? IdDetalleModificacionPoa { get; set; }

        public int IdModificacionPoa { get; set; }

        public int IdPartida { get; set; }

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

        public decimal? Observacion { get; set; }

        public int? CodigoActividad { get; set; }

        public virtual ModificacionPoa IdModificacionPoaNavigation { get; set; } = null!;

    }

}
