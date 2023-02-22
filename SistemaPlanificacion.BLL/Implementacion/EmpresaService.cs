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
    public class EmpresaService : IEmpresaService
    {
        private readonly IGenericRepository<Empresa> _repositorio;
        public EmpresaService(IGenericRepository<Empresa> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<List<Empresa>> Lista()
        {
            IQueryable<Empresa> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<Empresa> Crear(Empresa entidad)
        {
            try
            {
                Empresa empresa_creada=await _repositorio.Crear(entidad);
                if(empresa_creada.IdEmpresa==0)
                    throw new TaskCanceledException("No Se Pudo Crear La Empresa");
                return empresa_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Empresa> Editar(Empresa entidad)
        {
            try
            {
                Empresa empresa_encontrada=await _repositorio.Obtener(c=>c.IdEmpresa==entidad.IdEmpresa);
                empresa_encontrada.Codigo= entidad.Codigo;
                empresa_encontrada.Nombre=entidad.Nombre;
                empresa_encontrada.EsActivo = entidad.EsActivo;
                empresa_encontrada.FechaRegistro=entidad.FechaRegistro;
                bool respuesta=await _repositorio.Editar(empresa_encontrada);
                if(!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Empresa");
                return empresa_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idEmpresa)
        {
            try
            {
                Empresa empresa_encontrada = await _repositorio.Obtener(c => c.IdEmpresa == idEmpresa);
                if(empresa_encontrada==null)
                    throw new TaskCanceledException("La Empresa No Existe");
                bool respuesta = await _repositorio.Eliminar(empresa_encontrada);

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
