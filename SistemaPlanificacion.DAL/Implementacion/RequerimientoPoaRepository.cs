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

                    NumeroCorrelativoPoa correlativopoa = _dbContext.NumeroCorrelativoPoas.Where(n => n.Gestion == "requerimientoPoa").First();

                    correlativopoa.Ultimonumero++;

                    correlativopoa.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativoPoas.Update(correlativopoa);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativopoa.CantidadDigitos.Value));

                    string numeroRequerimientoPoa = ceros + correlativopoa.Ultimonumero.ToString();
                    numeroRequerimientoPoa = numeroRequerimientoPoa.Substring(numeroRequerimientoPoa.Length - correlativopoa.CantidadDigitos.Value, correlativopoa.CantidadDigitos.Value);

                    entidad.NumeroRequerimientoPoa = numeroRequerimientoPoa;

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
        public Task<List<DetalleRequerimientoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            throw new NotImplementedException();
        }
        public Task<bool> EliminarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad)
        {
            throw new NotImplementedException();
        }

        public Task<DetalleRequerimientoPoa> AgregarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad)
        {
            throw new NotImplementedException();

        }
    }
}
