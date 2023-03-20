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
    public class UnidadResponsableService : IUnidadresponsableService
    {
        private readonly IGenericRepository<UnidadResponsable> _repositorio;
        public UnidadResponsableService(IGenericRepository<UnidadResponsable> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<UnidadResponsable>> Lista()
        {
            IQueryable<UnidadResponsable> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<UnidadResponsable> Crear(UnidadResponsable entidad)
        {
            try
            {
                UnidadResponsable unidad_creada = await _repositorio.Crear(entidad);
                if (unidad_creada.IdUnidadResponsable == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Unidad Responsable");
                return unidad_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UnidadResponsable> Editar(UnidadResponsable entidad)
        {
            try
            {
                UnidadResponsable unidad_encontrada = await _repositorio.Obtener(c => c.IdUnidadResponsable == entidad.IdUnidadResponsable);
                unidad_encontrada.Codigo = entidad.Codigo;
                unidad_encontrada.Nombre = entidad.Nombre;
                unidad_encontrada.EsActivo = entidad.EsActivo;
                unidad_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(unidad_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Unidad Responsable");
                return unidad_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idUnidadResponsable)
        {
            try
            {
                UnidadResponsable unidad_encontrada = await _repositorio.Obtener(c => c.IdUnidadResponsable == idUnidadResponsable);
                if (unidad_encontrada == null)
                    throw new TaskCanceledException("La Unidad Responsable No Existe");
                bool respuesta = await _repositorio.Eliminar(unidad_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
