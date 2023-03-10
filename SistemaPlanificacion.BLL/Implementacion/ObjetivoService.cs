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
    public class ObjetivoService : IObjetivoService
    {
        private readonly IGenericRepository<Objetivo> _repositorio;
        public ObjetivoService(IGenericRepository<Objetivo> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Objetivo>> Lista()
        {
            IQueryable<Objetivo> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Objetivo> Crear(Objetivo entidad)
        {
            try
            {
                Objetivo objetivo_creada = await _repositorio.Crear(entidad);
                if (objetivo_creada.IdObjetivo == 0)
                    throw new TaskCanceledException("No Se Pudo Crear El Objetivo");
                return objetivo_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Objetivo> Editar(Objetivo entidad)
        {
            try
            {
                Objetivo objetivo_encontrado = await _repositorio.Obtener(c => c.IdObjetivo == entidad.IdObjetivo);
                objetivo_encontrado.Codigo = entidad.Codigo;
                objetivo_encontrado.Nombre = entidad.Nombre;
                objetivo_encontrado.EsActivo = entidad.EsActivo;
                objetivo_encontrado.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(objetivo_encontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar El Objetivo");
                return objetivo_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idObjetivo)
        {
            try
            {
                Objetivo objetivo_encontrado = await _repositorio.Obtener(c => c.IdObjetivo == idObjetivo);
                if (objetivo_encontrado == null)
                    throw new TaskCanceledException("El Objetivo No Existe");
                bool respuesta = await _repositorio.Eliminar(objetivo_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}
