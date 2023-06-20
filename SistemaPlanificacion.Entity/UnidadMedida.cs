using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class UnidadMedida
    {
        public UnidadMedida()
        {
            DetallePlanificacions = new HashSet<DetallePlanificacion>();
            DetalleRequerimientoPoas = new HashSet<DetalleRequerimientoPoa>();
            DetalleAnteproyectoPoas = new HashSet<DetalleAnteproyectoPoa>();
        }
        public int IdUnidad { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; set; }
        public virtual ICollection<DetalleRequerimientoPoa> DetalleRequerimientoPoas { get; set; }
        public virtual ICollection<DetalleAnteproyectoPoa> DetalleAnteproyectoPoas { get; set; }
    }
}
