using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PlanificacionService : IPlanificacionService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IPlanificacionRepository _repositorioPlanificacion;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;

        public PlanificacionService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, IPlanificacionRepository repositorioPlanificacion, IPartidapresupuestariaService partidapresupuestariaServicio)
        {
            _repositorioPartida = repositorioPartida;
            _repositorioPlanificacion = repositorioPlanificacion;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
        }

        public async Task<List<PartidaPresupuestaria>> ObtenerPartidas(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
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
                Planificacion planificacion_encontrada = await _repositorioPlanificacion.Obtener(c => c.IdPlanificacion == entidad.IdPlanificacion);
                planificacion_encontrada.EstadoCarpeta = entidad.EstadoCarpeta;
                planificacion_encontrada.FechaPlanificacion = entidad.FechaPlanificacion;
                bool respuesta = await _repositorioPlanificacion.Editar(planificacion_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Carpeta De Planificacion");
                return planificacion_encontrada;
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
                Planificacion planificacion_encontrada = await _repositorioPlanificacion.Obtener(c => c.IdPlanificacion == entidad.IdPlanificacion);
                planificacion_encontrada.EstadoCarpeta = entidad.EstadoCarpeta;
                planificacion_encontrada.FechaAnulacion = DateTime.Now;
                bool respuesta = await _repositorioPlanificacion.Editar(planificacion_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Anular La Carpeta De Planificacion");
                return planificacion_encontrada;
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
                    .ToList();

                /*
            .Include(g => g.Library.Select(h=>g.Book))
            .Include(j => j.Library.Select(k => k.Library.Select(l=>l.Book)))*/
            }
            else
            {
                return query.Where(p => p.NumeroPlanificacion == numeroPlanificacion)
                    .Include(tdp => tdp.IdDocumentoNavigation)
                    .Include(c=>c.IdCentroNavigation)
                    .Include(ur=>ur.IdUnidadResponsableNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacions)
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
                .ThenInclude (pp=>pp.IdPartidaNavigation)
                .ToList();
        }
    }
}
