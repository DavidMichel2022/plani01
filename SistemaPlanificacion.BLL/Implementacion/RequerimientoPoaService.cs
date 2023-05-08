﻿using Microsoft.EntityFrameworkCore;
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
    public class RequerimientoPoaService:IRequerimientoPoaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IGenericRepository<DetalleRequerimientoPoa> _repositorioDetalle;
        private readonly IGenericRepository<RequerimientoPoa> _repositorio;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;
        public RequerimientoPoaService(IGenericRepository<RequerimientoPoa> repositorio, IPartidapresupuestariaService partidapresupuestariaServicio, IGenericRepository<PartidaPresupuestaria> repositorioPartida, IGenericRepository<DetalleRequerimientoPoa> repositorioDetalle)
        {
            _repositorio = repositorio;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
            _repositorioPartida = repositorioPartida;
            _repositorioDetalle = repositorioDetalle;
        }

        public async Task<List<PartidaPresupuestaria>> ObtenerPartidas(string busqueda)
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartida.Consultar(p => p.EsActivo == true && string.Concat(p.Codigo, p.Nombre).Contains(busqueda));
            return query.Include(c => c.IdProgramaNavigation).ToList();
        }

        public async Task<List<RequerimientoPoa>> Lista()
        {
            IQueryable<RequerimientoPoa> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<RequerimientoPoa> Crear(RequerimientoPoa entidad)
        {
            try
            {               

                RequerimientoPoa requerimiento_creada = await _repositorio.Crear(entidad);
                if (requerimiento_creada.IdRequerimientoPoa == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Empresa");
                return requerimiento_creada;
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
                /*Empresa empresa_encontrada = await _repositorio.Obtener(c => c.IdEmpresa == entidad.IdEmpresa);
                empresa_encontrada.Codigo = entidad.Codigo;
                empresa_encontrada.Nombre = entidad.Nombre;
                empresa_encontrada.EsActivo = entidad.EsActivo;
                empresa_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(empresa_encontrada);
                if (!respuesta)*/
                    throw new TaskCanceledException("No Se Pudo Editar La Empresa");
              //  return empresa_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idRequerimiento)
        {
            try
            {
                RequerimientoPoa requerimiento_encontrada = await _repositorio.Obtener(c => c.IdRequerimientoPoa == idRequerimiento);
                if (requerimiento_encontrada == null)
                    throw new TaskCanceledException("El Requerimiento No Existe");
                bool respuesta = await _repositorio.Eliminar(requerimiento_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
