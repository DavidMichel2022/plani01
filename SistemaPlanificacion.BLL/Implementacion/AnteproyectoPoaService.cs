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
    public class AnteproyectoPoaService : IAnteproyectoPoaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IGenericRepository<UnidadMedida> _repositorioUnidad;
        private readonly IGenericRepository<DetalleAnteproyectoPoa> _repositorioDetalleAnteproyectoPoa;
        private readonly IAnteproyectoPoaRepository _repositorioAnteproyectoPoa;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;

        public AnteproyectoPoaService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, IGenericRepository<UnidadMedida> repositorioUnidad, IGenericRepository<DetalleAnteproyectoPoa> repositorioDetalleAnteproyectoPoa, IAnteproyectoPoaRepository repositorioAnteproyectoPoa, IPartidapresupuestariaService partidapresupuestariaServicio)
        {
            _repositorioPartida = repositorioPartida;
            _repositorioUnidad = repositorioUnidad;
            _repositorioDetalleAnteproyectoPoa = repositorioDetalleAnteproyectoPoa;
            _repositorioAnteproyectoPoa = repositorioAnteproyectoPoa;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
        }

        public async Task<List<AnteproyectoPoa>> Lista()
        {
            IQueryable<AnteproyectoPoa> query = await _repositorioAnteproyectoPoa.Consultar();
            return query
                .Include(c => c.IdCentroNavigation)
                .Include(ur => ur.IdUnidadResponsableNavigation)
                .Include(dr => dr.DetalleAnteproyectoPoas)
                .ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
        }
        public async Task<List<AnteproyectoPoa>> ListaPoaMiUnidad(int idUnidadResponsable)
        {
            IQueryable<AnteproyectoPoa> query = await _repositorioAnteproyectoPoa.Consultar(r => r.IdUnidadResponsable == idUnidadResponsable);
            var res = query
                 .Include(c => c.IdCentroNavigation)
                 .Include(ur => ur.IdUnidadResponsableNavigation)
                .Include(dr => dr.DetalleAnteproyectoPoas)
                .ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
            return res;
        }
        public async Task<AnteproyectoPoa> Crear(AnteproyectoPoa entidad)
        {
            try
            {

                AnteproyectoPoa anteproyectopoa_creada = await _repositorioAnteproyectoPoa.Crear(entidad);
                if (anteproyectopoa_creada.IdAnteproyecto == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Carpeta Anteproyecto Poa");
                return anteproyectopoa_creada;
            }
            catch
            {
                throw;
            }
        }
        public async Task<AnteproyectoPoa> Editar(AnteproyectoPoa entidad)
        {
            try
            {
                IQueryable<AnteproyectoPoa> query = await _repositorioAnteproyectoPoa.Consultar(p => p.IdAnteproyecto == entidad.IdAnteproyecto);

                var data = query
                        .Include(c => c.IdCentroNavigation)
                        .Include(ur => ur.IdUnidadResponsableNavigation)
                        .Include(u => u.IdUsuarioNavigation)
                        .Include(dp => dp.DetalleAnteproyectoPoas).ThenInclude(dpp => dpp.IdPartidaNavigation)
                        .FirstOrDefault();

                if (data != null)
                {
                    foreach (var objeto in data.DetalleAnteproyectoPoas)
                    {
                        DetalleAnteproyectoPoa detalle = objeto;
                        try
                        {
                            await _repositorioAnteproyectoPoa.EliminarDetalleAnteproyectoPoa(detalle);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                AnteproyectoPoa anteproyectopoa_para_editar = await _repositorioAnteproyectoPoa.Obtener(p => p.IdAnteproyecto == entidad.IdAnteproyecto);

                anteproyectopoa_para_editar.CiteAnteproyecto = entidad.CiteAnteproyecto;
                anteproyectopoa_para_editar.IdCentro = entidad.IdCentro;
                anteproyectopoa_para_editar.IdUnidadResponsable = entidad.IdUnidadResponsable;
                anteproyectopoa_para_editar.MontoAnteproyecto = entidad.MontoAnteproyecto;

                bool respuesta = await _repositorioAnteproyectoPoa.Editar(anteproyectopoa_para_editar);

                foreach (var objeto in entidad.DetalleAnteproyectoPoas)
                {
                    DetalleAnteproyectoPoa detalle = objeto;
                    try
                    {
                        detalle.IdAnteproyecto = anteproyectopoa_para_editar.IdAnteproyecto;
                        await _repositorioAnteproyectoPoa.AgregarDetalleAnteproyectoPoa(detalle);
                    }
                    catch
                    {
                        throw;
                    }
                }
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Carpeta De Anteproyecto Poa");
                return anteproyectopoa_para_editar;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(int idAnteproyecto)
        {
            try
            {
                AnteproyectoPoa anteproyectopoa_encontrada = await _repositorioAnteproyectoPoa.Obtener(p => p.IdAnteproyecto == idAnteproyecto);
                if (anteproyectopoa_encontrada == null)
                    throw new TaskCanceledException("No Se Pudo Eliminar La Carpeta De Anteproyecto Poa");

                bool respuesta = await _repositorioAnteproyectoPoa.Eliminar(anteproyectopoa_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<PartidaPresupuestaria>> ObtenerPartidasAnteproyecto(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
        }
        public async Task<List<UnidadMedida>> ObtenerUnidadesAnteproyecto(string busqueda)
        {
            IQueryable<UnidadMedida> query = await _repositorioUnidad.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.ToList();
        }
        public async Task<AnteproyectoPoa> Registrar(AnteproyectoPoa entidad)
        {
            try
            {
                return await _repositorioAnteproyectoPoa.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }
        public async Task<AnteproyectoPoa> Detalle(string citeAnteproyecto)
        {
            IQueryable<AnteproyectoPoa> query = await _repositorioAnteproyectoPoa.Consultar(p => p.CiteAnteproyecto == citeAnteproyecto);

            return query
                    .Include(c => c.IdCentroNavigation)
                    .Include(ur => ur.IdUnidadResponsableNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dr => dr.DetalleAnteproyectoPoas).ThenInclude(dpp => dpp.IdPartidaNavigation)
                    .First();
        }
        public async Task<AnteproyectoPoa> Anular(AnteproyectoPoa entidad)
        {
            try
            {
                AnteproyectoPoa anteproyectopoa_anulada = await _repositorioAnteproyectoPoa.Obtener(p => p.IdAnteproyecto == entidad.IdAnteproyecto);
                anteproyectopoa_anulada.EstadoAnteproyecto = entidad.EstadoAnteproyecto;
                anteproyectopoa_anulada.FechaAnulacion = DateTime.Now;
                bool respuesta = await _repositorioAnteproyectoPoa.Anular(anteproyectopoa_anulada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Anular La Carpeta De Anteproyecto Poa");
                return anteproyectopoa_anulada;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<AnteproyectoPoa>> ObtenerAnteproyectos(string citeAnteproyecto)
        {
            IQueryable<AnteproyectoPoa> query = await _repositorioAnteproyectoPoa.Consultar();
            return query.Where(rp => rp.CiteAnteproyecto == citeAnteproyecto).ToList();
        }
    }
}
