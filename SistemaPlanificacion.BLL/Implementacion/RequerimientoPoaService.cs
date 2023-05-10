using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class RequerimientoPoaService : IRequerimientoPoaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IGenericRepository<DetalleRequerimientoPoa> _repositorioDetalleRequerimientoPoa;
        private readonly IRequerimientoPoaRepository _repositorioRequerimientoPoa;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;

        public RequerimientoPoaService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, IRequerimientoPoaRepository repositorioRequerimientoPoa, IGenericRepository<DetalleRequerimientoPoa> repositorioDetalleRequerimientoPoa, IPartidapresupuestariaService partidapresupuestariaServicio)
        {
            _repositorioPartida = repositorioPartida;
            _repositorioDetalleRequerimientoPoa = repositorioDetalleRequerimientoPoa;
            _repositorioRequerimientoPoa = repositorioRequerimientoPoa;
            _repositorioDetalleRequerimientoPoa = repositorioDetalleRequerimientoPoa;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
        }

        public async Task<List<PartidaPresupuestaria>> ObtenerPartidas(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
        }

        public async Task<RequerimientoPoa> Registrar(RequerimientoPoa entidad)
        {
            try
            {
                return await _repositorioRequerimientoPoa.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }
        public async Task<RequerimientoPoa> Editar(RequerimientoPoa entidad)
        {
            try
            {
                IQueryable<RequerimientoPoa> query = await _repositorioRequerimientoPoa.Consultar(p => p.IdRequerimientoPoa == entidad.IdRequerimientoPoa);

                var data = query
                       // .Include(tdp => tdp.IdDocumentoNavigation)
                       // .Include(c => c.IdCentroNavigation)
                       // .Include(ur => ur.IdUnidadResponsableNavigation)
                       // .Include(u => u.IdUsuarioNavigation)
                        .Include(dp => dp.DetalleRequerimientoPoas).ThenInclude(dpp => dpp.IdPartidaNavigation)
                        .FirstOrDefault();

                if (data != null)
                {
                    foreach (var objeto in data.DetalleRequerimientoPoas)
                    {
                        DetalleRequerimientoPoa detalle = objeto;
                        try
                        {
                            await _repositorioRequerimientoPoa.EliminarDetalleRequerimientoPoa(detalle);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                RequerimientoPoa requerimientopoa_para_editar = await _repositorioRequerimientoPoa.Obtener(p => p.IdRequerimientoPoa == entidad.IdRequerimientoPoa);

                requerimientopoa_para_editar.CiteRequerimientoPoa = entidad.CiteRequerimientoPoa;
               // requerimientopoa_para_editar.IdDocumento = entidad.IdDocumento;
                requerimientopoa_para_editar.IdCentro = entidad.IdCentro;
                requerimientopoa_para_editar.IdUnidadResponsable = entidad.IdUnidadResponsable;
                requerimientopoa_para_editar.MontoPoa = entidad.MontoPoa;

                bool respuesta = await _repositorioRequerimientoPoa.Editar(requerimientopoa_para_editar);

                foreach (var objeto in entidad.DetalleRequerimientoPoas)
                {
                    DetalleRequerimientoPoa detalle = objeto;
                    try
                    {
                        detalle.IdRequerimientoPoa = requerimientopoa_para_editar.IdRequerimientoPoa;
                        await _repositorioRequerimientoPoa.AgregarDetalleRequerimientoPoa(detalle);
                    }
                    catch
                    {
                        throw;
                    }
                }
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Carpeta De Poa");
                return requerimientopoa_para_editar;
            }
            catch
            {
                throw;
            }
        }

        public async Task<RequerimientoPoa> Anular(RequerimientoPoa entidad)
        {
            try
            {
                RequerimientoPoa requerimientopoa_anulada = await _repositorioRequerimientoPoa.Obtener(p => p.IdRequerimientoPoa == entidad.IdRequerimientoPoa);
                requerimientopoa_anulada.EstadoRequerimientoPoa = entidad.EstadoRequerimientoPoa;
                requerimientopoa_anulada.FechaAnulacion = DateTime.Now;
                bool respuesta = await _repositorioRequerimientoPoa.Anular(requerimientopoa_anulada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Anular La Carpeta De Poa");
                return requerimientopoa_anulada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idRequerimientoPoa)
        {
            try
            {
                RequerimientoPoa requerimientopoa_encontrada = await _repositorioRequerimientoPoa.Obtener(p => p.IdRequerimientoPoa == idRequerimientoPoa);
                if (requerimientopoa_encontrada == null)
                    throw new TaskCanceledException("No Se Pudo Eliminar La Carpeta De Poa");

                bool respuesta = await _repositorioRequerimientoPoa.Eliminar(requerimientopoa_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RequerimientoPoa>> Lista()
        {
            IQueryable<RequerimientoPoa> query = await _repositorioRequerimientoPoa.Consultar();
            return query
              //  .Include(tdp => tdp.IdDocumentoNavigation)
               // .Include(c => c.IdCentroNavigation)
               // .Include(ur => ur.IdUnidadResponsableNavigation)
                //.Include(dp => dp.DetalleRequerimientoPoas)
                //.ThenInclude(dpp => dpp.IdPartidaNavigation)
                .Include(dp => dp.DetalleRequerimientoPoas).ThenInclude(dpp => dpp.IdPartidaNavigation)
                .ToList();
        }

        public async Task<RequerimientoPoa> Crear(RequerimientoPoa entidad)
        {
            try
            {

                RequerimientoPoa requerimientopoa_creada = await _repositorioRequerimientoPoa.Crear(entidad);
                if (requerimientopoa_creada.IdRequerimientoPoa == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Carpeta Poa");
                return requerimientopoa_creada;
            }
            catch
            {
                throw;
            }
        }

    }
}
