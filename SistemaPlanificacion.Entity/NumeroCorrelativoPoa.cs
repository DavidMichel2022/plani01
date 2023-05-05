using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.Entity
{
    public class NumeroCorrelativoPoa
    {
        public int IdCorrelativoPoa { get; set; }

        public int? UltimonumeroPoa { get; set; }

        public int? CantidadDigitosPoa { get; set; }

        public string? GestionPoa { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
