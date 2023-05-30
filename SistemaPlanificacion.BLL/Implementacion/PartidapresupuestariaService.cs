using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PartidapresupuestariaService : IPartidapresupuestariaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorio;
        public PartidapresupuestariaService(IGenericRepository<PartidaPresupuestaria> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<PartidaPresupuestaria>> Lista()
        {
            IQueryable<PartidaPresupuestaria> query = await _repositorio.Consultar();
            return query.Include(r => r.IdProgramaNavigation).ToList();
        }
        public async Task<PartidaPresupuestaria> Crear(PartidaPresupuestaria entidad)
        {
            PartidaPresupuestaria partida_existe = await _repositorio.Obtener(p => p.Codigo == entidad.Codigo);

            if (partida_existe != null)
                throw new TaskCanceledException("El Codigo Ya Existe");
            try
            {
                PartidaPresupuestaria partida_creada = await _repositorio.Crear(entidad);
                if (partida_creada.IdPartida == 0)
                    throw new TaskCanceledException("No Se Pudo Crear La Partida Presupuestaria");

                IQueryable<PartidaPresupuestaria> query = await _repositorio.Consultar(p => p.IdPartida == partida_creada.IdPartida);
                partida_creada=query.Include(r=>r.IdProgramaNavigation).First();

                return partida_creada;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PartidaPresupuestaria> Editar(PartidaPresupuestaria entidad)
        {
            PartidaPresupuestaria partida_existe = await _repositorio.Obtener(p => p.Codigo == entidad.Codigo && p.IdPartida != entidad.IdPartida);
            if (partida_existe != null)
                throw new TaskCanceledException("El Codigo Ya Existe");

            try
            {
                IQueryable<PartidaPresupuestaria> queryPartida=await _repositorio.Consultar(p=>p.IdPartida == entidad.IdPartida);

                PartidaPresupuestaria partida_para_editar=queryPartida.First();

                partida_para_editar.Codigo= entidad.Codigo;
                partida_para_editar.Nombre= entidad.Nombre;
                partida_para_editar.IdPrograma= entidad.IdPrograma;
                partida_para_editar.Stock= entidad.Stock;
                partida_para_editar.Precio= entidad.Precio;
                partida_para_editar.FechaRegistro= entidad.FechaRegistro;
                partida_para_editar.EsActivo= entidad.EsActivo;


                bool respuesta = await _repositorio.Editar(partida_para_editar);
                if (!respuesta)
                    throw new TaskCanceledException("No Se Pudo Editar La Partida Presupuestaria");

                PartidaPresupuestaria partida_editada = queryPartida.Include(r => r.IdProgramaNavigation).First();

                return partida_editada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idPartida)
        {
            try
            {
                PartidaPresupuestaria partida_encontrada = await _repositorio.Obtener(c => c.IdPartida == idPartida);
                if (partida_encontrada == null)
                    throw new TaskCanceledException("La Partida Presupuestaria No Existe");

                bool respuesta = await _repositorio.Eliminar(partida_encontrada);


                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PartidaPresupuestaria> ObtenerPartidaPresupuestariaByCodigo(string codigo)
        {
            try
            {
                PartidaPresupuestaria partida_encontrada = await _repositorio.Obtener(p => p.Codigo.Contains(codigo));
                return partida_encontrada;
            }
            catch
            {
                throw;
            }
        }
    }
}