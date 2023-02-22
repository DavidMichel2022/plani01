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
    public class TipodocumentoService : ITipodocumentoService
    {
        private readonly IGenericRepository<TipoDocumento> _repositorio;
        public TipodocumentoService(IGenericRepository<TipoDocumento> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TipoDocumento>> Lista()
        {
            IQueryable<TipoDocumento> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<TipoDocumento> Crear(TipoDocumento entidad)
        {
            try
            {
                TipoDocumento tipodocumento_creada = await _repositorio.Crear(entidad);
                if (tipodocumento_creada.IdDocumento == 0)
                    throw new TaskCanceledException("No Se Pudo Crear El Tipo De Documento");
                return tipodocumento_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TipoDocumento> Editar(TipoDocumento entidad)
        {
            try
            {
                TipoDocumento tipodocumento_encontrada = await _repositorio.Obtener(c => c.IdDocumento == entidad.IdDocumento);
                tipodocumento_encontrada.Codigo= entidad.Codigo;
                tipodocumento_encontrada.Descripcion = entidad.Descripcion;
                tipodocumento_encontrada.EsActivo = entidad.EsActivo;
                tipodocumento_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(tipodocumento_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar El Tipo De Documento");
                return tipodocumento_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idDocumento)
        {
            try
            {
                TipoDocumento tipodocumento_encontrada = await _repositorio.Obtener(c => c.IdDocumento == idDocumento);
                if (tipodocumento_encontrada == null)
                    throw new TaskCanceledException("El Tipo De Documento No Existe");
                bool respuesta = await _repositorio.Eliminar(tipodocumento_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}