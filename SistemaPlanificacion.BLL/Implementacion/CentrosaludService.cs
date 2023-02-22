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
    public class CentrosaludService : ICentrosaludService
    {
        private readonly IGenericRepository<CentroSalud> _repositorio;
        public CentrosaludService(IGenericRepository<CentroSalud> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<List<CentroSalud>> Lista()
        {
            IQueryable<CentroSalud> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<CentroSalud> Crear(CentroSalud entidad)
        {
            try
            {
                CentroSalud centro_creado = await _repositorio.Crear(entidad);
                if (centro_creado.IdCentro == 0)
                    throw new TaskCanceledException("No Se Pudo Crear El Centro De Salud");
                return centro_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CentroSalud> Editar(CentroSalud entidad)
        {
            try
            {
                CentroSalud centro_encontrado = await _repositorio.Obtener(c => c.IdCentro == entidad.IdCentro);
                centro_encontrado.Codigo= entidad.Codigo;
                centro_encontrado.Nombre = entidad.Nombre;
                centro_encontrado.EsActivo = entidad.EsActivo;
                centro_encontrado.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(centro_encontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar El Centro De Salud");
                return centro_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idCentro)
        {
            try
            {
                CentroSalud centro_encontrado = await _repositorio.Obtener(c => c.IdCentro == idCentro);
                if (centro_encontrado == null)
                    throw new TaskCanceledException("El Centro De Salud No Existe");
                bool respuesta = await _repositorio.Eliminar(centro_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
