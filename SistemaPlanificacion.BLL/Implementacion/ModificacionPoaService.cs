using Microsoft.EntityFrameworkCore;
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
    public class ModificacionPoaService:IModificacionPoaService
    {
        private readonly IGenericRepository<ModificacionPoa> _repositorio;
        public ModificacionPoaService(IGenericRepository<ModificacionPoa> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ModificacionPoa>> Lista()
        {
            IQueryable<ModificacionPoa> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<ModificacionPoa> Crear(ModificacionPoa entidad)
        {
            try
            {
                ModificacionPoa modificacionPoa_creado = await _repositorio.Crear(entidad);
                if (modificacionPoa_creado.IdModificacionPoa == 0)
                    throw new TaskCanceledException("No Se Pudo Crear la solicitu de Modificacion del Poa");
                return modificacionPoa_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModificacionPoa> Editar(ModificacionPoa entidad)
        {
            try
            {
                ModificacionPoa modificacionPoa_encontrado = await _repositorio.Obtener(c => c.IdModificacionPoa == entidad.IdModificacionPoa);
                //modificacionPoa_encontrado.Codigo = entidad.Codigo;
                //modificacionPoa_encontrado.Nombre = entidad.Nombre;
                //modificacionPoa_encontrado.EsActivo = entidad.EsActivo;
                //modificacionPoa_encontrado.FechaRegistro = entidad.FechaRegistro;
                bool respuesta = await _repositorio.Editar(modificacionPoa_encontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar la solicitud de Modificación Poa");
                return modificacionPoa_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idModificacionPoa)
        {
            try
            {
                ModificacionPoa modificacionPoa_encontrado = await _repositorio.Obtener(c => c.IdModificacionPoa == idModificacionPoa);
                if (modificacionPoa_encontrado== null)
                    throw new TaskCanceledException("la solicitud de Modificacion Poa No Existe");
                bool respuesta = await _repositorio.Eliminar(modificacionPoa_encontrado);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        public async Task<ModificacionPoa> ObtenerModificacionPoa(int idModificacionPoa)
        {
            IQueryable<ModificacionPoa> query = await _repositorio.Consultar();
            return query.Where(p => p.IdModificacionPoa == idModificacionPoa)
                 .Include(dp => dp.DetalleModificacions)
                 .FirstOrDefault();
        }
    }
}
