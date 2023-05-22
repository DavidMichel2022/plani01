using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;


namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PlanificacionService : IPlanificacionService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IGenericRepository<DetallePlanificacion> _repositorioDetalle;
        private readonly IPlanificacionRepository _repositorioPlanificacion;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;

        public PlanificacionService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, IPlanificacionRepository repositorioPlanificacion, IGenericRepository<DetallePlanificacion> repositorioDetalle, IPartidapresupuestariaService partidapresupuestariaServicio)
        {
            _repositorioPartida = repositorioPartida;
            _repositorioDetalle = repositorioDetalle;
            _repositorioPlanificacion = repositorioPlanificacion;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
        }

        public async Task<List<PartidaPresupuestaria>> ObtenerPartidas(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
        }

        public async Task<List<Planificacion>> ObtenerPlanificaciones(string citePlanificacion)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar();
            return query.Where(p => p.CitePlanificacion == citePlanificacion).ToList();
        }
        public async Task<Planificacion> Registrar(Planificacion entidad)
        {
            try
            {
                return await _repositorioPlanificacion.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Planificacion> Editar(Planificacion entidad)
        {
            try
            {
                IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar(p => p.IdPlanificacion == entidad.IdPlanificacion);

                var data = query
                        .Include(tdp => tdp.IdDocumentoNavigation)
                        .Include(c => c.IdCentroNavigation)
                        .Include(ur => ur.IdUnidadResponsableNavigation)
                        .Include(u => u.IdUsuarioNavigation)
                        .Include(dp => dp.DetallePlanificacions).ThenInclude(dpp => dpp.IdPartidaNavigation)
                        .FirstOrDefault();     
                
                if (data != null)
                {
                   // List<DetallePlanificacion> listDetalle = planificacion_encontrada.DetallePlanificacions.ToList();
                    foreach (var objeto in data.DetallePlanificacions)
                    {
                        DetallePlanificacion detalle = objeto;
                        try
                        {
                            await _repositorioPlanificacion.EliminarDetallePlanificacion(detalle);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                Planificacion planificacion_para_editar = await _repositorioPlanificacion.Obtener(p => p.IdPlanificacion == entidad.IdPlanificacion);

                planificacion_para_editar.CitePlanificacion = entidad.CitePlanificacion;
                planificacion_para_editar.IdDocumento = entidad.IdDocumento;
                planificacion_para_editar.IdCentro = entidad.IdCentro;
                planificacion_para_editar.IdUnidadResponsable = entidad.IdUnidadResponsable;
                planificacion_para_editar.MontoPlanificacion = entidad.MontoPlanificacion;

                bool respuesta = await _repositorioPlanificacion.Editar(planificacion_para_editar);

                foreach(var objeto in entidad.DetallePlanificacions)
                {
                    DetallePlanificacion detalle = objeto;
                    try
                    {
                        detalle.IdPlanificacion = planificacion_para_editar.IdPlanificacion;
                        await _repositorioPlanificacion.AgregarDetallePlanificacion(detalle);
                    }
                    catch
                    {
                        throw;
                    }
                }
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Carpeta De Planificacion");
                return planificacion_para_editar;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Planificacion> Anular(Planificacion entidad)
        {
            try
            {
                Planificacion planificacion_anulada = await _repositorioPlanificacion.Obtener(p => p.IdPlanificacion == entidad.IdPlanificacion);
                planificacion_anulada.EstadoCarpeta = entidad.EstadoCarpeta;
                planificacion_anulada.FechaAnulacion = DateTime.Now;
                bool respuesta = await _repositorioPlanificacion.Anular(planificacion_anulada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Anular La Carpeta De Planificacion");
                return planificacion_anulada;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(int idPlanificacion)
        {
            try
            {
                Planificacion planificacion_encontrada = await _repositorioPlanificacion.Obtener(p => p.IdPlanificacion == idPlanificacion);
                if (planificacion_encontrada == null)
                    throw new TaskCanceledException("No Se Pudo Eliminar La Carpeta De Planificacion");

                bool respuesta = await _repositorioPlanificacion.Eliminar(planificacion_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Planificacion>> Historial(string numeroPlanificacion, string fechaInicio, string fechaFin)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar();

            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if(fechaInicio != "" && fechaFin != "")
            {
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(p => p.FechaPlanificacion.Value.Date >= fech_inicio.Date && p.FechaPlanificacion.Value.Date <= fech_fin.Date)
                    .Include(tdp => tdp.IdDocumentoNavigation)
                    .Include(c => c.IdCentroNavigation)
                    .Include(ur => ur.IdUnidadResponsableNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacions)
                    .ThenInclude(dpp => dpp.IdPartidaNavigation)
                    .ToList();
            }
            else
            {
                return query.Where(p => p.NumeroPlanificacion == numeroPlanificacion)
                    .Include(tdp => tdp.IdDocumentoNavigation)
                    .Include(c=>c.IdCentroNavigation)
                    .Include(ur=>ur.IdUnidadResponsableNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacions)
                    .ThenInclude(dpp => dpp.IdPartidaNavigation)
                    .ToList();
            }
        }

        public async Task<Planificacion> Detalle(string numeroPlanificacion)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar(p => p.NumeroPlanificacion == numeroPlanificacion);
            
            return query
                    .Include(tdp => tdp.IdDocumentoNavigation)
                    .Include(c => c.IdCentroNavigation)
                    .Include(ur => ur.IdUnidadResponsableNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacions).ThenInclude(dpp => dpp.IdPartidaNavigation)
                    .First();
        }

        public async Task<List<DetallePlanificacion>> Reporte(string fechaInicio, string fechaFin)
        {
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

            List<DetallePlanificacion> lista = await _repositorioPlanificacion.Reporte(fech_inicio, fech_fin);
            return lista;
        }
        public async Task<List<Planificacion>> Lista()
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar();
            return query
                .Include(tdp => tdp.IdDocumentoNavigation)
                .Include(c => c.IdCentroNavigation)
                .Include(ur => ur.IdUnidadResponsableNavigation)
                .Include(dp => dp.DetallePlanificacions)
                .ThenInclude(dpp=>dpp.IdPartidaNavigation)
                //.Include(dp => dp.DetallePlanificacions).ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
        }

        public async Task<List<Planificacion>> ListaCertificarPlanificacion()
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar(p => p.EstadoCarpeta == "INI" || p.EstadoCarpeta=="PLA");
            return query
                .Include(tdp => tdp.IdDocumentoNavigation)
                .Include(c => c.IdCentroNavigation)
                .Include(ur => ur.IdUnidadResponsableNavigation)
                .Include(dp => dp.DetallePlanificacions)
                //.ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
        }

        public async Task<bool> EliminarDetalles(int idPlanificacion)
        {
            try
            {
                Planificacion planificacion_encontrada = await _repositorioPlanificacion.Obtener(p => p.IdPlanificacion == idPlanificacion);
                if (planificacion_encontrada == null)
                    throw new TaskCanceledException("No Se Pudo Eliminar La Carpeta De Planificacion");
                else
                {
                    List <DetallePlanificacion> listDetalle = planificacion_encontrada.DetallePlanificacions.ToList();
                    foreach (var detalle in listDetalle)
                    {
                        detalle.IdDetallePlanificacion = idPlanificacion;
                        var idDetalle = detalle.IdDetallePlanificacion;
                    }                    
                }
                bool respuesta = await _repositorioPlanificacion.Eliminar(planificacion_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Planificacion>> ListaCarpetasxUsuario(int idUsuarioActivo)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar();

            return query.Where(p => p.IdUsuario == idUsuarioActivo)
                .Include(tdp => tdp.IdDocumentoNavigation)
                .Include(c => c.IdCentroNavigation)
                .Include(ur => ur.IdUnidadResponsableNavigation)
                .Include(dp => dp.DetallePlanificacions)
                .ThenInclude(dpp => dpp.IdPartidaNavigation)
                //.Include(dp => dp.DetallePlanificacions).ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
        }

        public async Task<List<PartidaPresupuestaria>> ObtenerPartidasPlanificacion(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
        }
    }
}
