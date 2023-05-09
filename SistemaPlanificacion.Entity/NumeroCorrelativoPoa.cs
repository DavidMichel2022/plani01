using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class NumeroCorrelativoPoa
    {
        public int IdCorrelativo { get; set; }

        public int? Ultimonumero { get; set; }

        public int? CantidadDigitos { get; set; }

        public string? Gestion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
