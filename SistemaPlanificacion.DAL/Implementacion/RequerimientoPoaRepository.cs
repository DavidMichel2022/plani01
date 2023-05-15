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

namespace SistemaPlanificacion.DAL.Implementacion
{
    public class RequerimientoPoaRepository : GenericRepository<RequerimientoPoa>, IRequerimientoPoaRepository
    {
        private readonly BasePlanificacionContext _dbContext;
        public RequerimientoPoaRepository(BasePlanificacionContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RequerimientoPoa> Registrar(RequerimientoPoa entidad)
        {
            RequerimientoPoa requerimientopoaGenerada = new RequerimientoPoa();


            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.RequerimientoPoas.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    requerimientopoaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return requerimientopoaGenerada;
        }
        public async Task<List<DetalleRequerimientoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleRequerimientoPoa> listaResumen = await _dbContext.DetalleRequerimientoPoas
                .Include(dpp => dpp.IdPartidaNavigation)
                .Include(p => p.IdRequerimientoPoaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(p => p.IdRequerimientoPoaNavigation)
                .ThenInclude(c => c.IdCentroNavigation)
                .Include(p => p.IdRequerimientoPoaNavigation)
                .Where(dp => dp.IdRequerimientoPoaNavigation.FechaRequerimientoPoa.Value.Date >= FechaInicio.Date && dp.IdRequerimientoPoaNavigation.FechaRequerimientoPoa.Value.Date <= FechaFin.Date).ToListAsync();
            return listaResumen;
        }
        public async Task<bool> EliminarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad)
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

        public async Task<DetalleRequerimientoPoa> AgregarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad)
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
