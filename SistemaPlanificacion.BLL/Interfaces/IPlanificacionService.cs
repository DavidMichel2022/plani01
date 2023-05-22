using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IPlanificacionService
    {
        Task<List<PartidaPresupuestaria>> ObtenerPartidas(string busqueda);
        Task<Planificacion> Registrar(Planificacion entidad);
        Task<Planificacion> Editar(Planificacion entidad);
        Task<Planificacion> Anular(Planificacion entidad);
        Task<bool> Eliminar(int idPlanificacion);
        Task<bool> EliminarDetalles(int idPlanificacion);
        Task<List<Planificacion>> Historial(string numeroPlanificacion, string fechaInicio, string fechaFin);
        Task<Planificacion> Detalle(string numeroPlanificacion);
        Task<List<DetallePlanificacion>> Reporte(string fechaInicio, string fechaFin);
        Task<List<Planificacion>> Lista();
        Task<List<Planificacion>> ListaCertificarPlanificacion();
        Task<List<Planificacion>> ListaCarpetasxUsuario(int idUsuarioActivo);
        Task<List<PartidaPresupuestaria>> ObtenerPartidasPlanificacion(string busqueda);
        Task<List<Planificacion>> ObtenerPlanificaciones(string busqueda);
    }
}
