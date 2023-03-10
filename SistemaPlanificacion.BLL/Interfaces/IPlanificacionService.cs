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
        Task<List<Actividad>> ObtenerActividades(string busqueda);
        Task<List<CentroSalud>> ObtenerCentrosSalud(string busqueda);
        Task<List<Operacion>> ObtenerOperaciones(string busqueda);
        Task<List<Objetivo>> ObtenerObjetivos(string busqueda);
        Task<List<TablaAce>> ObtenerTablasAce(string busqueda);
        Task<List<UnidadResponsable>> ObtenerUnidadesResponsables(string busqueda);
        Task<Planificacion> Registrar(Planificacion entidad);
        Task<List<Planificacion>> Historial(string numeroPlanificacion, string fechaInicio, string fechaFin);
        Task<Planificacion> Detalle(string numeroPlanificacion);
        Task<List<DetallePlanificacion>> Reporte(string fechaInicio, string fechaFin);
    }
}
