using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.DAL.Interfaces
{
    public interface IPlanificacionRepository:IGenericRepository<Planificacion>
    {
        Task<Planificacion> Registrar(Planificacion entidad);
        Task<List<DetallePlanificacion>> Reporte(DateTime FechaInicio, DateTime FechaFin);
    }
}
