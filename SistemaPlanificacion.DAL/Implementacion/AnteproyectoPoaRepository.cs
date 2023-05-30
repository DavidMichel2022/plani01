using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.DAL.DBContext;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;

namespace SistemaPlanificacion.DAL.Implementacion
{
    public class AnteproyectoPoaRepository : GenericRepository<AnteproyectoPoa>, IAnteproyectoPoaRepository
    {
        private readonly BasePlanificacionContext _dbContext;
        public AnteproyectoPoaRepository(BasePlanificacionContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AnteproyectoPoa> Registrar(AnteproyectoPoa entidad)
        {
            AnteproyectoPoa anteproyectopoaGenerada = new();


            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.AnteproyectoPoas.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    anteproyectopoaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return anteproyectopoaGenerada;
        }
        public async Task<List<DetalleAnteproyectoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleAnteproyectoPoa> listaResumen = await _dbContext.DetalleAnteproyectoPoas
                .Include(dpp => dpp.IdPartidaNavigation)
                .Include(p => p.IdAnteproyectoPoaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(p => p.IdAnteproyectoPoaNavigation)
                .ThenInclude(c => c.IdCentroNavigation)
                .Include(p => p.IdAnteproyectoPoaNavigation)
                .Where(ap => ap.IdAnteproyectoPoaNavigation.FechaAnteproyecto.Value.Date >= FechaInicio.Date && ap.IdAnteproyectoPoaNavigation.FechaAnteproyecto.Value.Date <= FechaFin.Date).ToListAsync();
            return listaResumen;
        }
        public async Task<bool> EliminarDetalleAnteproyectoPoa(DetalleAnteproyectoPoa entidad)
        {
            try
            {
                _dbContext.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleAnteproyectoPoa> AgregarDetalleAnteproyectoPoa(DetalleAnteproyectoPoa entidad)
        {
            try
            {
                _dbContext.Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }
    }
}
