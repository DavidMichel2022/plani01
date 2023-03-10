using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class Objetivo
    {
        public Objetivo()
        {
            PlanificacionPrograma = new HashSet<Planificacion>();
        }

        public int IdObjetivo { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }

        public bool? EsActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<DocmPlanificacion> DocmPlanificacion { get; } = new List<DocmPlanificacion>();

        public virtual ICollection<Planificacion> Planificacion { get; } = new List<Planificacion>();

        public virtual ICollection<Presupuesto> Presupuesto { get; } = new List<Presupuesto>();

        public virtual ICollection<Planificacion> PlanificacionPrograma { get; } = new List<Planificacion>();
    }
}
