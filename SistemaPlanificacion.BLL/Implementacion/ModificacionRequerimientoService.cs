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
    public class ModificacionRequerimientoService : IModificacionRequerimientoService
    {
        private readonly IGenericRepository<ModificacionRequerimiento> _repositorio;

        public ModificacionRequerimientoService(IGenericRepository<ModificacionRequerimiento> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<ModificacionRequerimiento> Crear(ModificacionRequerimiento entidad)
        {
            try
            {
                ModificacionRequerimiento modificacionRequerimiento_creada = await _repositorio.Crear(entidad);
                if (modificacionRequerimiento_creada.IdModificacionRequerimiento == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La modificacionRequerimiento");
                return modificacionRequerimiento_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModificacionRequerimiento> Editar(ModificacionRequerimiento entidad)
        {
            try
            {
                ModificacionRequerimiento modificacionRequerimiento_creada = await _repositorio.Obtener(c => c.IdModificacionRequerimiento == entidad.IdModificacionRequerimiento);
                /*empresa_encontrada.Codigo = entidad.Codigo;
                empresa_encontrada.Nombre = entidad.Nombre;
                empresa_encontrada.EsActivo = entidad.EsActivo;
                empresa_encontrada.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(empresa_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Empresa");
                */
                return modificacionRequerimiento_creada;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> Eliminar(int idModificacionRequerimiento)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ModificacionRequerimiento>> Lista()
        {

            IQueryable<ModificacionRequerimiento> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<List<ModificacionRequerimiento>> ListaModificadosSolicitud(int idSolicitudModificacion)
        {
            IQueryable<ModificacionRequerimiento> query = await _repositorio.Consultar();            
            return query.Where(p => p.IdModificacionPoa == idSolicitudModificacion)
                 //.Include(dp => dp.)
                 .ToList();
        }
    }
}
