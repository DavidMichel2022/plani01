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
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IGenericRepository<UnidadMedida> _repositorio;
        public UnidadMedidaService(IGenericRepository<UnidadMedida> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<List<UnidadMedida>> Lista()
        {
            IQueryable<UnidadMedida> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<UnidadMedida> Crear(UnidadMedida entidad)
        {
            try
            {
                UnidadMedida unidadmedida_creada = await _repositorio.Crear(entidad);
                if (unidadmedida_creada.IdUnidad == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Unidad Medida");
                return unidadmedida_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UnidadMedida> Editar(UnidadMedida entidad)
        {
            try
            {
                UnidadMedida unidadmedida_encontrada = await _repositorio.Obtener(c => c.IdUnidad == entidad.IdUnidad);
                unidadmedida_encontrada.Codigo = entidad.Codigo;
                unidadmedida_encontrada.Nombre = entidad.Nombre;
                unidadmedida_encontrada.EsActivo = entidad.EsActivo;
                unidadmedida_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(unidadmedida_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Unidad De Medida");
                return unidadmedida_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idUnidad)
        {
            try
            {
                UnidadMedida unidadmedida_encontrada = await _repositorio.Obtener(c => c.IdUnidad == idUnidad);
                if (unidadmedida_encontrada == null)
                    throw new TaskCanceledException("La Unidad De Medida No Existe");
                bool respuesta = await _repositorio.Eliminar(unidadmedida_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
