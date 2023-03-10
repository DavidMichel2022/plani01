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
    public class OperacionService : IOperacionService
    {
        private readonly IGenericRepository<Operacion> _repositorio;
        public OperacionService(IGenericRepository<Operacion> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Operacion>> Lista()
        {
            IQueryable<Operacion> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Operacion> Crear(Operacion entidad)
        {
            try
            {
                Operacion operacion_creada = await _repositorio.Crear(entidad);
                if (operacion_creada.IdOperacion == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Operación");
                return operacion_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Operacion> Editar(Operacion entidad)
        {
            try
            {
                Operacion operacion_encontrada = await _repositorio.Obtener(c => c.IdOperacion == entidad.IdOperacion);
                operacion_encontrada.Codigo = entidad.Codigo;
                operacion_encontrada.Nombre = entidad.Nombre;
                operacion_encontrada.EsActivo = entidad.EsActivo;
                operacion_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(operacion_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Operación");
                return operacion_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idOperacion)
        {
            try
            {
                Operacion operacion_encontrada = await _repositorio.Obtener(c => c.IdOperacion == idOperacion);
                if (operacion_encontrada == null)
                    throw new TaskCanceledException("La Operación No Existe");
                bool respuesta = await _repositorio.Eliminar(operacion_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}
