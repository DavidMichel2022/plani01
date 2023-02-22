using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PartidapresupuestariaService : IPartidapresupuestariaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorio;
        public PartidapresupuestariaService(IGenericRepository<PartidaPresupuestaria> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<PartidaPresupuestaria>> Lista()
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<PartidaPresupuestaria> Crear(PartidaPresupuestaria entidad)
        {
            try
            {
                PartidaPresupuestaria partidapresupuestaria_creada = await _repositorio.Crear(entidad);
                if (partidapresupuestaria_creada.IdPartida == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Partida Presupuestaria");
                return partidapresupuestaria_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PartidaPresupuestaria> Editar(PartidaPresupuestaria entidad)
        {
            try
            {
                PartidaPresupuestaria partidapresupuestaria_encontrada = await _repositorio.Obtener(c => c.IdPartida == entidad.IdPartida);
                partidapresupuestaria_encontrada.Codigo = entidad.Codigo;
                partidapresupuestaria_encontrada.Nombre = entidad.Nombre;
                partidapresupuestaria_encontrada.EsActivo = entidad.EsActivo;
                partidapresupuestaria_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(partidapresupuestaria_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Partida Presupuestaria");
                return partidapresupuestaria_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idPartida)
        {
            try
            {
                PartidaPresupuestaria partidapresupuestaria_encontrada = await _repositorio.Obtener(c => c.IdPartida == idPartida);
                if (partidapresupuestaria_encontrada == null)
                    throw new TaskCanceledException("La Partida Presupuestaria No Existe");
                bool respuesta = await _repositorio.Eliminar(partidapresupuestaria_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}