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
        private readonly IGenericRepository<Actividad> _repositorioActividad;
        private readonly IGenericRepository<Operacion> _repositorioOperacion;
        private readonly IGenericRepository<Objetivo> _repositorioObjetivo;
        private readonly IGenericRepository<TablaAce> _repositorioTablaace;
        private readonly IGenericRepository<CentroSalud> _repositorioCentrosalud;
        private readonly IGenericRepository<UnidadResponsable> _repositorioUnidadesResponsables;
        private readonly IPlanificacionRepository _repositorioPlanificacion;

        public PlanificacionService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, IGenericRepository<Actividad> repositorioActividad, IGenericRepository<Operacion> repositorioOperacion, IGenericRepository<Objetivo> repositorioObjetivo, IGenericRepository<TablaAce> repositorioTablaace, IGenericRepository<CentroSalud> repositorioCentrosalud, IGenericRepository<UnidadResponsable> repositorioUnidadesResponsables, IPlanificacionRepository repositorioPlanificacion)
        {
            _repositorioPartida = repositorioPartida;
            _repositorioActividad = repositorioActividad;
            _repositorioOperacion = repositorioOperacion;
            _repositorioObjetivo = repositorioObjetivo;
            _repositorioTablaace = repositorioTablaace;
            _repositorioCentrosalud = repositorioCentrosalud;
            _repositorioUnidadesResponsables = repositorioUnidadesResponsables;
            _repositorioPlanificacion = repositorioPlanificacion;
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
        public async Task<List<Planificacion>> Historial(string numeroPlanificacion, string fechaInicio, string fechaFin)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar();

            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if(fechaInicio!="" && fechaFin != "")
            {
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(p => p.FechaPlanificacion.Value.Date >= fech_inicio.Date && p.FechaPlanificacion.Value.Date <= fech_fin.Date)
                    .Include(tdp => tdp.IdPlanificacionTipoDocumentoNavigation)
                    .Include(u => u.IdPlanificacionUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacion)
                    .ToList();
            }
            else
            {
                return query.Where(p => p.NumeroPlanificacion == numeroPlanificacion)
                    .Include(tdp => tdp.IdPlanificacionTipoDocumentoNavigation)
                    .Include(u => u.IdPlanificacionUsuarioNavigation)
                    .Include(dp => dp.DetallePlanificacion)
                    .ToList();
            }
        }

        public async Task<Planificacion> Detalle(string numeroPlanificacion)
        {
            IQueryable<Planificacion> query = await _repositorioPlanificacion.Consultar(p => p.NumeroPlanificacion == numeroPlanificacion);
            
            return query
                  .Include(tdp => tdp.IdPlanificacionTipoDocumentoNavigation)
                  .Include(u => u.IdPlanificacionUsuarioNavigation)
                  .Include(dp => dp.DetallePlanificacion)
                  .First();
        }

        public async Task<List<DetallePlanificacion>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

            List<DetallePlanificacion> lista = await _repositorioPlanificacion.Reporte(fech_inicio, fech_fin);
            return lista;
        }

        public async Task<List<Actividad>> ObtenerActividades(string busqueda)
        {
            IQueryable<Actividad> query = await _repositorioActividad.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }

        public async Task<List<CentroSalud>> ObtenerCentrosSalud(string busqueda)
        {
            IQueryable<CentroSalud> query = await _repositorioCentrosalud.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }

        public async Task<List<Operacion>> ObtenerOperaciones(string busqueda)
        {
            IQueryable<Operacion> query = await _repositorioOperacion.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }

        public async Task<List<Objetivo>> ObtenerObjetivos(string busqueda)
        {
            IQueryable<Objetivo> query = await _repositorioObjetivo.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }

        public async Task<List<TablaAce>> ObtenerTablasAce(string busqueda)
        {
            IQueryable<TablaAce> query = await _repositorioTablaace.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }

        public async Task<List<UnidadResponsable>> ObtenerUnidadesResponsables(string busqueda)
        {
            IQueryable<UnidadResponsable> query = await _repositorioUnidadesResponsables.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }
    }
}
