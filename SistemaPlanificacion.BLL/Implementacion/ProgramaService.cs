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
    public class ProgramaService : IProgramaService
    {
        private readonly IGenericRepository<Programa> _repositorio;
        public ProgramaService(IGenericRepository<Programa> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Programa>> Lista()
        {
            IQueryable<Programa> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Programa> Crear(Programa entidad)
        {
            try
            {
                Programa programa_creado = await _repositorio.Crear(entidad);
                if (programa_creado.IdPrograma == 0)
                    throw new TaskCanceledException("No Se Pudo Crear El Programa");
                return programa_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Programa> Editar(Programa entidad)
        {
            try
            {
                Programa programa_encontrado = await _repositorio.Obtener(c => c.IdPrograma == entidad.IdPrograma);
                programa_encontrado.Codigo= entidad.Codigo;
                programa_encontrado.Nombre = entidad.Nombre;
                programa_encontrado.EsActivo = entidad.EsActivo;
                programa_encontrado.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(programa_encontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar El Programa");
                return programa_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idPrograma)
        {
            try
            {
                Programa programa_encontrado = await _repositorio.Obtener(c => c.IdPrograma == idPrograma);
                if (programa_encontrado == null)
                    throw new TaskCanceledException("El Programa No Existe");
                bool respuesta = await _repositorio.Eliminar(programa_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
