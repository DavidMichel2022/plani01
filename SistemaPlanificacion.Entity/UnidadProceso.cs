using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class UnidadProceso
    {
        public int IdUnidadProceso { get; set; }
        public string? Nombre { get; set; }
        public string? Abrevia { get; set; }

        public bool? EsActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
