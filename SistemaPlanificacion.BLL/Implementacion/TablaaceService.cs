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
    public class TablaaceService : ITablaaceService
    {
        private readonly IGenericRepository<TablaAce> _repositorio;
        public TablaaceService(IGenericRepository<TablaAce> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TablaAce>> Lista()
        {
            IQueryable<TablaAce> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<TablaAce> Crear(TablaAce entidad)
        {
            try
            {
                TablaAce tablaAce_creada = await _repositorio.Crear(entidad);
                if (tablaAce_creada.IdAce == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Tabla ACE");
                return tablaAce_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TablaAce> Editar(TablaAce entidad)
        {
            try
            {
                TablaAce tablaAce_encontrada = await _repositorio.Obtener(c => c.IdAce == entidad.IdAce);
                tablaAce_encontrada.Codigo = entidad.Codigo;
                tablaAce_encontrada.Nombre = entidad.Nombre;
                tablaAce_encontrada.EsActivo = entidad.EsActivo;
                tablaAce_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(tablaAce_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Tabla ACE");
                return tablaAce_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idAce)
        {
            try
            {
                TablaAce tablaAce_encontrada = await _repositorio.Obtener(c => c.IdAce == idAce);
                if (tablaAce_encontrada == null)
                    throw new TaskCanceledException("La Tabla ACE No Existe");
                bool respuesta = await _repositorio.Eliminar(tablaAce_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
