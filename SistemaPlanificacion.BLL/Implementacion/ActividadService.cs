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
    public class ActividadService : IActividadService
    {
        private readonly IGenericRepository<Actividad> _repositorio;
        public ActividadService(IGenericRepository<Actividad> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Actividad>> Lista()
        {
            IQueryable<Actividad> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Actividad> Crear(Actividad entidad)
        {
            try
            {
                Actividad actividad_creada = await _repositorio.Crear(entidad);
                if (actividad_creada.IdActividad == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Actividad");
                return actividad_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Actividad> Editar(Actividad entidad)
        {
            try
            {
                Actividad actividad_encontrada = await _repositorio.Obtener(c => c.IdActividad == entidad.IdActividad);
                actividad_encontrada.Codigo= entidad.Codigo;
                actividad_encontrada.Nombre = entidad.Nombre;
                actividad_encontrada.EsActivo = entidad.EsActivo;
                actividad_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(actividad_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Actividad");
                return actividad_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idActividad)
        {
            try
            {
                Actividad actividad_encontrada = await _repositorio.Obtener(c => c.IdActividad == idActividad);
                if (actividad_encontrada == null)
                    throw new TaskCanceledException("La Actividad No Exite");
                bool respuesta = await _repositorio.Eliminar(actividad_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
