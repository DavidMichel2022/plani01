using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RequerimientoPoa requerimientoPoaGenerada = new RequerimientoPoa();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    NumeroCorrelativoPoa correlativopoa = _dbContext.NumeroCorrelativoPoas.Where(n => n.GestionPoa == "requerimientoPoa").First();

                    //correlativo.Ultimonumero = correlativo.Ultimonumero + 1;
                    correlativopoa.UltimonumeroPoa++;
                    correlativopoa.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativoPoas.Update(correlativopoa);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativopoa.CantidadDigitosPoa.Value));

                    string numeroRequerimientoPoa = ceros + correlativopoa.UltimonumeroPoa.ToString();
                    numeroRequerimientoPoa = numeroRequerimientoPoa.Substring(numeroRequerimientoPoa.Length - correlativopoa.CantidadDigitosPoa.Value, correlativopoa.CantidadDigitosPoa.Value);

                    entidad.NumeroRequerimientoPoa = numeroRequerimientoPoa;

                    await _dbContext.RequerimientoPoas.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    requerimientoPoaGenerada = entidad;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return requerimientoPoaGenerada;
        }

        public async Task<List<DetalleRequerimientoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleRequerimientoPoa> listaResumen = await _dbContext.DetalleRequerimientoPoas
                //////.Include(dpp => dpp.IdPartidaNavigation)
                .Include(p => p.IdRequerimientoPoaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(p => p.IdRequerimientoPoaNavigation)
                .ThenInclude(c => c.IdCentroNavigation)
                .Where(dp => dp.IdRequerimientoPoaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date && dp.IdRequerimientoPoaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();
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
